namespace InstaFlow
open Fable.React
open Fable.Core
[<AutoOpen>]
module Globals =
  open Elmish.UrlParser
  open Fable.MaterialUI.Themes
  open Fable.MaterialUI.Core
  type Page =
    | Home
    | Login
    | NotFound

  let toPage = function
    | Home -> "/home"
    | Login -> "/login"
    | _ -> "/404"

  let pageParser : Parser<Page-> Page,_> =
      oneOf
        [ map Home (s "home")
          map Home (s "/")
          map Login (s "login")]

  let inline styled styles f model =
            ReactElementType.create (withStyles<IClassesProps> (StyleType.Styles styles) [] (f model) ) JsInterop.createEmpty []

  let inline elmView name render =
    FunctionComponent.Of( render,name)
  let inline elmLazy placeholder render =
    FunctionComponent.Lazy(render, placeholder)

  let elmStyled name styles f  =
      elmView name (styled styles f)