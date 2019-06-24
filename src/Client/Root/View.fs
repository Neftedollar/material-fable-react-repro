module InstaFlow.WebClient.Root.View
open InstaFlow.WebClient
open Fable.React
open Fable.React.Props
open Thoth.Json
open Types
open InstaFlow.Utils.PushStateLink
open InstaFlow.Globals
open InstaFlow.WebClient



let cstmView   =
  FunctionComponent.Of((fun mdl ->
  div [] [
    h1 [ ] [ str (if mdl.CurrentPage = Home then  "HOME" else "Login")]
  ]), "app")





let view model dispatch =
    div []
        [
          Navigation.View.view (model.CurrentPage.ToString())
          alink [ To (Page.Home |> toPage) ] [ str "Home"]
          br []
          alink [ To (Page.Login |> toPage) ] [ str "Login"]
          br []
          cstmView model
          br []
          br [] ]
