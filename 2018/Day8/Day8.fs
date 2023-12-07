open System
open System.IO

let parseInput (input:string) = 
    input.Split(" ".ToCharArray()) |> Array.map int

let readNodeInfo (input:int list) = 
    match input with
    | child::data::next ->
        (child, data, next)

let rec parseTree (input:int list) = 
    let newNode = readNodeInfo input
    match newNode with
    | (0, d, next ) -> (next |> List.take d |> List.sum, d+2)
    | (n, d, next) -> 
        Array.fold (
            fun state item ->
                let result = parseTree (next |> List.skip (snd state))
                if item = n
                then 
                    let currentMetadataSum = (next |> List.skip ((snd result)+(snd state)) |> List.take d |> List.sum)
                    ((fst result) + currentMetadataSum+(fst state), (snd result)+(snd state)+2+d)
                else
                    (fst result+(fst state) , (snd result)+(snd state))
            ) (0, 0) [| 1 .. n |] 


let getNodeSum (input:int list) (childs: (int*int) array) =
    input |> List.sumBy(
        fun x ->
            if x <= childs.Length
            then childs |> Array.where (fun c -> (fst c + 1) = x) |> Array.head |> snd
            else 0
        )

let rec parseTree2 (input:int list) = 
    let newNode = readNodeInfo input
    match newNode with
    | (0, d, next ) -> (next |> List.take d |> List.sum, d+2)
    | (n, d, next) -> 
        let r = Array.mapFold (fun state item ->
            let result = parseTree2 (next |> List.skip state)
            ((item, fst result), (state + snd result))) 0 [| 0 .. n-1 |]
        ( getNodeSum ( input |> List.skip ((snd r)+2) |> List.take d ) (fst r), (snd r)+d+2)
                     
[<EntryPoint>]
let main argv =
    let input = File.ReadAllText "input.txt" |> parseInput |> List.ofArray
    //parseTree [3; 3; 1; 1; 0; 3; 1; 1; 1; 4; 0; 4; 1; 1; 1; 1; 2; 2; 0; 1; 1; 1; 1; 0; 1; 3; 1; 2; 3; 0; 0; 9] 0 |> printf "%A" 
    parseTree input |> fst |> printf "%i\n" 
    //parseTree2 [3; 3; 1; 1; 0; 3; 1; 1; 1; 4; 0; 4; 1; 1; 1; 1; 2; 2; 0; 1; 1; 1; 1; 0; 1; 3; 1; 2; 3; 3; 3; 9] |> printf "%A"
    parseTree2 input |> printf "%A"
    0 // return an integer exit code
