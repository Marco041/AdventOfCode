open System.IO
open System
open System.Threading

type Point = {
    Distance: Int32
    Coord: Int32*Int32
    NearestInput: (Int32*Int32) option
}

let parse (input:String) =
    let point = input.Split(", ".ToCharArray())
    (point.[0] |> int, point.[2] |> int)

let manDist (p1:int*int) (p2:int*int) =
    Math.Abs(fst p1 - fst p2) + Math.Abs(snd p1 - snd p2)

let computeDistance (inputPoints:(int*int) array) (currentX:int) (currentY:int) =
    inputPoints |> Array.fold (fun state point -> 
        let dist = manDist point (currentX,currentY)
        if dist < state.Distance
        then {Distance=dist; Coord=(currentX,currentY); NearestInput=Some(point)}
        else if dist=state.Distance
            then {Distance=state.Distance; Coord=(currentX,currentY); NearestInput=None}
            else state
    ) {Distance=Int32.MaxValue; Coord=(0,0); NearestInput=None}

let rec computeDistance2 points currentPoint limit currentSum =
    match points with
    | x::xs ->
        let dist = (manDist x currentPoint)
        if currentSum + dist < limit 
        then computeDistance2 xs currentPoint limit (currentSum+dist)
        else false
    | [] -> true

[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines "input.txt" |> Array.map parse
    let heigth = input |> Array.maxBy fst |> fst
    let width = input |> Array.maxBy snd |> snd
    let grid = Array2D.init heigth width (computeDistance input)
    let topRow = grid.[0,*] |> Array.map (fun x->x.NearestInput) |> Array.choose id
    let bottomRow = grid.[heigth-1,*] |> Array.map (fun x->x.NearestInput) |> Array.choose id
    let leftCol = grid.[*,0] |> Array.map (fun x->x.NearestInput) |> Array.choose id
    let rightCol = grid.[*,width-1] |> Array.map (fun x->x.NearestInput) |> Array.choose id

    let t = Array.concat [| topRow; bottomRow; leftCol; rightCol |] |> Set.ofArray

    let fg = grid |> Seq.cast<Point> |> Seq.filter (fun x -> Option.isSome(x.NearestInput) && not (Set.contains x.NearestInput.Value t)) 
    let p1 = fg |> Seq.choose (fun x -> x.NearestInput) |> Seq.countBy id |> Seq.maxBy snd
    
    printf "%A\n" p1
    let p2 = 
        grid |> Seq.cast<Point>  
        |> Seq.fold (
            fun sum item ->
                if computeDistance2 (input |> List.ofArray) item.Coord 10000 0
                then sum+1
                else sum
            ) 0
    printf "%i\n" p2
    0 // return an integer exit code
