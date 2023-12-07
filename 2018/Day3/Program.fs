// Learn more about F# at http://fsharp.org

open System
open System.IO

type Claim = {
    id: int;
    coordStart: int*int;
    coordEnd: int*int
}


let parser (line:string) =
    let pt = line.Split(" ,x:@#".ToCharArray())
    { 
        id = pt.[1] |> int;
        coordStart = (pt.[4] |> int, pt.[5] |> int);
        coordEnd = (pt.[7] |> int, pt.[8] |> int)    
    }

let calcArea (claim:Claim) =
    let x1 = fst claim.coordStart
    let y1 = snd claim.coordStart
    seq{
        for y in y1..(y1+snd claim.coordEnd-1) do
            for x in x1..(x1+fst claim.coordEnd-1) do
                yield (claim.id, (x,y))
    }

let countPipeline input =
    input
    |> Array.map calcArea
    |> Seq.concat
    |> Seq.groupBy (snd)
    
    

let part1 input=
    input
    |> Seq.filter(fun x -> (snd x) |> Seq.length > 1)
    |> Seq.length

let part2 input = 
    input 
    |> Seq.map(fun x -> 
            if ((snd x) |> Seq.length > 1) 
                then (snd x) |> Seq.map fst
                else Seq.empty
        )
    |> Seq.filter (fun x -> x |> Seq.length>0)
    |> Seq.concat
    |> Seq.distinct
    |> Set.ofSeq
        

    
[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines "input.txt" |> Array.map parser
    let count = input |> countPipeline
    count |> part1 |> printf "%i"
    let t = count |> part2 
    (t - (input |> Array.map(fun x-> x.id) |> Set.ofArray )) |> printf "%A" 
    0