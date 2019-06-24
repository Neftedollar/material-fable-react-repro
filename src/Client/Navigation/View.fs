module InstaFlow.WebClient.Navigation.View
open Fable.React
open Fable.React.Props
open Fable.MaterialUI.Core
open Fable.MaterialUI.Props
open Fable.MaterialUI.Themes
open Fable.Core.JsInterop
open InstaFlow
open Fable.Core
open Globals
let private styles : IStyles list = [
  Styles.Root [
    FlexGrow 1
  ]
  Styles.Custom
    ("flex", [
        FlexGrow 1 ])
  Styles.Custom
    ("menuButton", [
        CSSProp.MarginLeft -12
        CSSProp.MarginRight 20 ])
]
let appBar = fun currentPage (props:IClassesProps) ->
  let classes = props.classes
  div [ Class !!classes?root ] [
    appBar [ AppBarProp.Position AppBarPosition.Static ] [
      toolbar [ ToolbarProp.Variant ToolbarVariant.Dense] [
        iconButton [
          Class !!classes?menuButton
          MaterialProp.Color ComponentColor.Inherit
          HTMLAttr.Custom ("aria-label", "Menu") ] [
            icon [] [str "menu"] ]
        typography [
          Class !!classes?flex
          TypographyProp.Variant TypographyVariant.H6
          MaterialProp.Color ComponentColor.Inherit
        ] [ str currentPage ]
        button [
          MaterialProp.Color ComponentColor.Inherit
        ] [ str "Login"]
      ]
    ]
  ]

let view =

  FunctionComponent.Of( fun model -> ReactElementType.create (withStyles<IClassesProps> (StyleType.Styles styles) [] (appBar model) ) JsInterop.createEmpty [] )

//and this is working
// let view model =
//   ReactElementType.create (withStyles<IClassesProps> (StyleType.Styles styles) [] (appBar model) ) JsInterop.createEmpty []