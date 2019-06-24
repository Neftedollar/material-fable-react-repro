module InstaFlow.WebClient.Root.App

open Elmish
open Elmish.React
open Elmish.Streams
open FSharp.Control
open Elmish.UrlParser


module Server =

    open InstaFlow.Shared
    open Fable.Remoting.Client

    /// A proxy you can use to talk to server directly
    let api : CounterApi =
      Remoting.createApi()
      |> Remoting.withRouteBuilder Route.builder
      |> Remoting.buildProxy<CounterApi>

open Types
open Elmish.Navigation
open InstaFlow.Globals

      // map Home (s "blog" </> i32)
      // map Search (s "search" </> str) ]

let urlUpdate result (model:Model) =
  (match result with
  | Some page ->
      {model with CurrentPage = page}
  | None ->
      {model with CurrentPage = Home }), Cmd.none

// defines the initial state
let init page  =
    { CurrentPage = Option.defaultValue Page.Home page  }
// The update function computes the next state of the application based on the current state and the incoming events/messages
let update (msg : Msg) (currentModel : Model)  =
    match  msg with
    | _ -> currentModel

let load = AsyncRx.ofAsync (Server.api.initialCounter ())

let stream model msgs =
    // match model.Counter with
    // | None ->  loadCount
    // | _ -> msg
    msgs

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif


Program.mkSimple init update View.view
|> Program.withStream stream "msgs"
|> Program.toNavigable (parsePath pageParser) urlUpdate
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
