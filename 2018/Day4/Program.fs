open System
open System.IO
open System.Numerics

type State = Id of int | FromWake | FromSleep
type Guard = {
    Date : DateTime
    State : State
}

let parse (input:string) = 
    let inputList = input.Split("[]# ".ToCharArray())
    let date = ( String.concat " " [|inputList.[1];inputList.[2]|] ) |> System.DateTime.Parse
    let editDate = 
        match date.Hour, date.Minute with
        | (23,i) -> date.AddMinutes((60 % date.Minute) |> float)
        | (0,0) -> date.AddSeconds(1.0)
        | _ -> date
    let state =
        match inputList.[4] with
        | "Guard" -> Id (inputList.[6] |> int)
        | "falls" -> FromWake
        | "wakes" -> FromSleep
    (editDate, state)

let orderInput (input:(DateTime*State)[]) =
    input 
    |> Array.sortBy fst
    |> Array.fold (
        fun state x->
            match (snd x) with
            | Id i -> Array.append state [| (i, (fst x, snd x)) |]
            | _ -> Array.append state [| (fst (state |> Array.last), (fst x, snd x)) |]
        ) Array.empty
    |> Array.groupBy fst

let commonCalculator func (x:(int*(DateTime*State))[])  =
        x |> Array.map snd |> Array.pairwise  |> func

let findMinute minStart minEnd (minuteDistr : int[]) =
    [| for i in 0 .. 59 -> 
        if i>=minStart && i < minEnd 
            then minuteDistr.[i]+1 
            else minuteDistr.[i] |]

let calc2  (x : ((DateTime*State)*(DateTime*State))[]) =
    x |> Array.fold(
            fun acc x -> 
            match ((snd (fst x)), (snd (snd x))) with
            | (FromWake, FromSleep) -> findMinute (fst (fst x)).Minute (fst (snd x)).Minute acc
            | _ -> acc
        ) [| for i in 0 .. 59 -> 0 |]

let calc1  (x : ((DateTime*State)*(DateTime*State))[]) =
    x |> Array.fold(
            fun acc x -> 
            match ((snd (fst x)), (snd (snd x))) with
            | (FromWake, FromSleep) -> acc + (fst (snd x)).Minute - (fst (fst x)).Minute
            | _ -> acc
        ) 0
    
[<EntryPoint>]
let main argv =
    let line = File.ReadAllLines "input.txt" |> Array.map parse |> orderInput

    let result1 = 
        line 
        |> Array.map( fun x -> (fst x, commonCalculator calc1 (snd x))) 
        |> Array.maxBy snd

    let result1_2 =
        line 
        |> Array.filter (fun x -> fst x = fst result1) 
        |> Array.head 
        |> snd
        |> commonCalculator calc2
        |> Seq.mapi (fun i v -> i, v)
        |> Seq.maxBy snd
        |> fst

    let result2 =
        line 
        |> Array.map (fun x -> (fst x, commonCalculator calc2 (snd x)))
        |> Array.map(
            fun x ->
                snd x
                |> Array.mapi (fun i v -> i, v)
                |> fun item -> (fst x, item |> Array.maxBy (snd)))
        |> Array.maxBy (snd >> snd)
    printf "%A %i %A" result1 result1_2 result2    
    0