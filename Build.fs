open Fake.Core
open Fake.IO

open Helpers


initializeContext ()


let clientPath = Path.getFullName "src/Client"
let deployPath = Path.getFullName "deploy"


Target.create "clean" (fun _ ->
    Shell.cleanDir deployPath
    Shell.cleanDir (Path.combine clientPath "dist")
    run dotnet [ "fable"; "clean"; "--yes"; "-e"; ".jsx" ] clientPath // Delete *.fs.js files created by Fable
)


Target.create "restoreclient" (fun _ -> run npm [ "ci" ] clientPath)


Target.create "bundle" (fun _ ->
    [
        "client",
        dotnet
            [
                "fable"
                "-o"
                "output"
                "-s"
                "-e"
                ".jsx"
                "--run"
                "npx"
                "vite"
                "build"
                "--emptyOutDir"
            ]
            clientPath
    ]
    |> runParallel)



Target.create "run" (fun _ ->
    [
        "client", dotnet [ "fable"; "watch"; "-o"; "output"; "-s"; "-e"; ".jsx"; "--run"; "npx"; "vite" ] clientPath
    ]
    |> runParallel)


Target.create "Format" (fun _ -> run dotnet [ "fantomas"; "." ] ".")


open Fake.Core.TargetOperators


let dependencies =
    [
        "clean" ==> "restoreclient" ==> "bundle"
        "clean" ==> "restoreclient" ==> "run"
    ]


[<EntryPoint>]
let main args = runOrDefault args
