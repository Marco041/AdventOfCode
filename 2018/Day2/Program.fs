// Learn more about F# at http://fsharp.org

open System
open System.IO
open System.Collections



let part1 (input : string[]) =

    let containsElements (num:int) (input : seq<Tuple<Char,Int32>>)  =
        if (input |> Seq.map snd |> Seq.contains num) 
        then 1 
        else 0

    let cnt = 
        fun fl -> 
            input 
            |> Array.map (fun (s:string) -> s |> Seq.countBy id |> containsElements fl)

    (cnt 2 |> Seq.sum) * (cnt 3 |> Seq.sum)

let part2 input =

    let check x1 x2 =
            (Seq.fold2 (
                fun acc a b ->
                    if (a = b) then acc + string(a)
                    else acc
            ) "" x1 x2)

    let correctLength = (input |> Array.head |> Seq.length) - 1

    let res = input 
                |> Array.map(
                    fun item -> 
                        input 
                        |> Array.filter(fun item2 -> (check item item2).Length = correctLength))
                |> Array.filter (fun x -> x.Length >0)   
                     
    check res.[0].[0] res.[1].[0]

[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines "input.txt"
    part1 input |> printf "%i\n"
    part2 input |> printf "%s"
    0 // return an integer exit code