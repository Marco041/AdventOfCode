open System
open System.IO
open System.Xml.Linq
open System.ComponentModel

type Nodes = {
    Prevs: string list;
    Nexts: string list;
}

type Worker = {
  WorkingElement: string;
  Time: int;
}

let parseInput (input:string) =
    let listSplit = input.Split(" ".ToCharArray())
    (listSplit.[1], listSplit.[7])

let buildInputMap (input:(string*string)[]) =
  let m = 
    input 
    |> Array.groupBy fst 
    |> Array.map (
        fun (x, ys) -> 
            x, 
            { Nexts = [for _, y in ys -> y]; 
              Prevs= 
                input 
                |> Array.filter(fun inputItem -> (snd inputItem) = x ) 
                |> Array.map fst 
                |> List.ofArray}) 
    |> Map.ofArray
  let lastNode = input |> Array.filter (fun x -> not (Map.containsKey (snd x) m))
  let newMap = Map.add (snd lastNode.[0]) {Prevs = (lastNode |> Array.map fst) |> List.ofArray; Nexts = []} m
  newMap

let removeDups (is:string list) =
    let d = System.Collections.Generic.Dictionary()
    [ for i in is do match d.TryGetValue i with
                     | (false,_) -> d.[i] <- (); yield i
                     | _ -> () ]

let listDiff (a:string list) (b:string list) =
  (Set.ofList b) - (Set.ofList a)

let rec computeNextSequence (elements:Map<string,Nodes>) (nextSequence:string list) (currentSequence:string list) = 
    let newSeq = nextSequence |> List.sort
    match newSeq with
    | x::xs -> 
        let node = (elements.TryFind x).Value
        if node.Prevs.IsEmpty || (listDiff currentSequence node.Prevs).IsEmpty
          then computeNextSequence elements (removeDups (List.append xs node.Nexts)) (removeDups (List.append currentSequence [x]))
          else computeNextSequence elements xs currentSequence
    | [] -> currentSequence

let getSequenceNode (map:Map<string,Nodes>) =
    let firstNode = map |> Map.filter(fun x v -> v.Prevs.IsEmpty) |> Map.toList |> List.map fst |> List.sort
    let current =  firstNode.Head
    computeNextSequence map (List.append firstNode.Tail (map.TryFind current).Value.Nexts) [firstNode.Head]


let rec computeNextSequence2 (elements:Map<string,Nodes>) (nextSequence:string list) (currentSequence:string list) (currentElement:string) = 
    let currentNode = (elements.TryFind currentElement).Value
    let toAdd = 
      currentNode.Nexts 
      |> List.filter (fun x -> 
          let nd = (elements.TryFind x).Value
          (nd.Prevs |> List.filter (fun y -> not (List.contains y currentSequence))).IsEmpty)
    toAdd

let getElementTime ch = 
  (ch |> char |> int)-4

let rec workerHandler elements (currentJobs:Worker list) (nextSequence:string list) (currentSequence:string list) (totaleTime:int) =
  if currentJobs.Length = 0 && nextSequence.Length=0
  then totaleTime
  else
    if currentJobs.Length >= 5 || nextSequence.Length = 0
    then
      let workerEnd = currentJobs |> List.minBy (fun x -> x.Time)
      let finishedJobs = currentJobs |> List.map (fun x ->{WorkingElement = x.WorkingElement; Time = x.Time-(workerEnd.Time)}) |> List.sortBy (fun x -> x.Time) |> List.tail
      let newSequence = (List.append currentSequence [workerEnd.WorkingElement])
      match computeNextSequence2 elements nextSequence newSequence workerEnd.WorkingElement with
      | x::xs -> workerHandler elements ({Time= getElementTime x; WorkingElement=x}::finishedJobs) xs newSequence (totaleTime+workerEnd.Time)
      | [] -> workerHandler elements finishedJobs nextSequence newSequence (totaleTime+workerEnd.Time)
    else
      match nextSequence with
        | x::xs -> workerHandler elements ({Time= getElementTime x; WorkingElement=x}::currentJobs) xs currentSequence totaleTime
        | [] -> workerHandler elements currentJobs nextSequence currentSequence totaleTime

let getSequenceNode2 (map:Map<string,Nodes>) =
    let firstNode = map |> Map.filter(fun x v -> v.Prevs.IsEmpty) |> Map.toList
    let workers = firstNode |> List.take 4 |> List.map (fun x -> {Time = getElementTime (fst x); WorkingElement = (fst x)})
    workerHandler map workers List.empty List.Empty 0


[<EntryPoint>]
let main argv =
    let input = File.ReadAllLines "input.txt" |> Array.map parseInput
    let map = buildInputMap input
    getSequenceNode map |> List.map (printf "%s") |> ignore
    getSequenceNode2 map |> printf "\n%i\n"
    0
