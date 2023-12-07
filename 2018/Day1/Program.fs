// Learn more about F# at http://fsharp.org

open System
open System.IO

let (%!) a b = (a % b + b) % b

let solvePart2 changes =
    let shift = Array.sum changes
    let getDiff ((xi, xf), (yi, yf)) = if shift > 0 then (yf - xf, xi, yf) else (yf - xf, yi, xf)
    changes
    |> Array.scan (+) 0
    |> Array.take (Seq.length changes)
    |> Array.mapi (fun i v -> (i, v))
    |> Array.groupBy (fun x -> (snd x) % shift)
    |> Array.map(fun g -> snd g |> (Array.sortBy(fun (x, y) -> y)) |> Array.pairwise |> Array.map getDiff)
    //|> Array.map(fun g -> (snd g) |> Array.map snd |> Array.sort |> Array.pairwise |> (Array.map (fun x -> (snd x) - (fst x))))
    //|> Array.map(fun g -> snd g |> Array.sort |> Array.pairwise |> (Array.map (fun x -> Math.Abs((snd x) - (fst x)))))
    |> Array.concat
    |> Array.min

// let findDuplicates input =
//     input
//     |> Array.groupBy id
//     |> (Array.filter (fun (k,v) -> v.Length > 1))
//     |> Array.map (fun (_,y) -> (Array.head y))


let findDuplicates (input1:int[]) (input2:int[]) =
    seq { for item in input2 do
            if Array.contains item input1 then
                yield item
    }


let rec solvePart2Loop (input : int[]) (oldFreq : int[]) (currentSum : int) =
    let newValue = input |> Array.scan (+) currentSum
    let newFreq = newValue |> Array.take input.Length
    let newSum = Array.last newValue
    let res = findDuplicates oldFreq newFreq
    if Seq.isEmpty res then 
        solvePart2Loop input (Array.append oldFreq newFreq) newSum
    else 
        Seq.head res

[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines "input.txt" |> Array.map int
    input |> Array.sum  |> printf "%i\n"
    input |> (solvePart2  >> (fun (_, _, f) -> f)) |> printf "%i\n"
    (solvePart2Loop input Array.empty 0) |> printf "%A"
    0
