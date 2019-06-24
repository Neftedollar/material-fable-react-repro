module InstaFlow.Utils.PushStateLink
open Fable.React
open Fable.React.Props
open Fable.React.Standard
open Fable.Core
open Fable.Core.JsInterop
open Browser.Types
open Browser
open Fable.Import

type LinkProp =
    | To of string
    | Replace of bool
    interface IHTMLProp

type LinkAttributes<'a> = {
  ``to`` : string
  target : string option
  replace : bool
  onClick : (Browser.Types.MouseEvent -> 'a) option
}

let private isModifiedEvent (e : MouseEvent) =
    e.metaKey || e.altKey || e.ctrlKey || e.shiftKey

let alink (props : IHTMLProp list) children =
    let p : LinkAttributes<_> = keyValueList CaseRules.LowerFirst props |> unbox
    let href = p.``to``
    let newProps : IHTMLProp list = [
        Href href
        OnClick (fun e ->
            match p.onClick with
            | Some handler ->
                handler e
            | None ->
                if not e.defaultPrevented && e.button = 0.
                    && (Option.isNone p.target || p.target = Some "_self") && not (isModifiedEvent e) then
                    e.preventDefault()

                    if p.replace then Browser.Dom.history.replaceState ((), "", href)
                    else Browser.Dom.history.pushState ((), "", href)

                    let ev = CustomEvent.Create("NavigatedEvent")
                    ev.initEvent ("NavigatedEvent", true, true)
                    Browser.Dom.window.dispatchEvent ev |> ignore
            )
    ]
    let restProps =
        props
        |> List.filter (fun i ->
            match i with
            | :? LinkProp -> false
            | :? HTMLAttr as a ->
                match a with
                | Href _ -> false
                | _ -> true
            | _ -> true)
    a (newProps @ restProps) children