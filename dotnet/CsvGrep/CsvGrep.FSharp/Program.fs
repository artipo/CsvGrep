// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let filePath = argv.[0]
    let index = argv.[1] |> int
    let value = argv.[2]
    
    Console.WriteLine("Result:")
    
    File.ReadAllLines filePath
    |> List.ofArray
    |> List.map (fun r -> r.Replace(";", ""))
    |> List.filter (fun r -> r.Split(",").[index] = value)
    |> List.iter (fun r -> Console.WriteLine($"{r};"))
    
    0 // return an integer exit code