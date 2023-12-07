open System
open System.IO

let reducePart1 (input:seq<char>) = 
    input |> Seq.fold ( 
        fun (acc:char list) (ch:char) ->
            match acc with
            | x::xs when abs((x|>int)-(ch|>int)) = 32 -> xs
            | xs -> ch::xs
    ) List.empty

let calcPart2 (input:seq<char>) =
    input |> Set.ofSeq |> Set.map Char.ToLower
    |> Set.map(fun x ->
        input 
        |> Seq.filter (fun ch-> (ch |> Char.ToLower) <> x ) 
        |> reducePart1 
        |> List.length)
    |> Set.minElement

[<EntryPoint>]
let main argv =
    let res1 = File.ReadAllText "input.txt" |> reducePart1
    res1 |> List.length |> printf "%i\n"
    res1 |> calcPart2 |> printf "%A"
    0
