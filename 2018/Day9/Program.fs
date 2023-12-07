// Learn more about F# at http://fsharp.org

open System

let insertItemBetween index item elements=
    let splitted = Array.splitAt (index) elements
    Array.concat [|(fst splitted); [|item|]; (snd splitted)|]

let rec calcNewIndex currentIndex (shift:int) length =
    let sg = Math.Sign shift
    if sg <> 0
    then 
        let newIndex = currentIndex + sg
        if newIndex > length
            then calcNewIndex 1 (shift - sg) length
            elif newIndex < 0
                then calcNewIndex (length - 1) (shift - sg) length
                else calcNewIndex newIndex (shift - sg) length
    else currentIndex

let rec play (marble:int) (currentMarbleIndex:int) (board:int array) =
    match marble % 23 with
    | 0 ->
        let newIndex1 = calcNewIndex currentMarbleIndex (-6) board.Length
        let newIndex2 = calcNewIndex currentMarbleIndex (-8) board.Length
        let newItem = board.[calcNewIndex newIndex 1 board.Length]
        play newItem newIndex (insertItemBetween newIndex1 marble board)
    | _ ->
        let newIndex = calcNewIndex currentMarbleIndex (+2) board.Length
        play (marble + 1) newIndex (insertItemBetween newIndex marble board)

[<EntryPoint>]
let main argv =
    let player = 10
    let finalPoint = 1618
    play 1 0 [|0|]
    0 // return an integer exit code
