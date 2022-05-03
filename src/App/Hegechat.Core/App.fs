module Hegechat.Core.App

open Sutil
open DOM
open Attr
open Bulma

module EventHelpers =
    open Browser.Types

    let inputElement (target:EventTarget) = target |> asElement<HTMLInputElement>

    let validity (e : Event) =
        inputElement(e.target).validity

type Model =
    { Counter : int
      Text: string }

type Message =
    | Increment
    | Decrement
    | SetText of string

let init () =
    { Counter = 0
      Text = "Init" }, []

let update msg model =
    match msg with
    | Increment -> { model with Counter = model.Counter + 1 }, Cmd.none
    | Decrement -> { model with Counter = model.Counter - 1 }, Cmd.none
    | SetText t -> { model with Text = t }, Cmd.none

let app() =
   
    let model, dispatch = () |> Store.makeElmish init update ignore
    
    Html.div [
        disposeOnUnmount [ model ]
        
        style [
            Css.fontFamily "Arial, Helvetica, sans-serif"
            Css.margin 20
        ]
        
        Html.h1 "Hello Hegechat"
        
        Bind.fragment (Store.map (fun m -> m.Counter) model) (fun n -> Html.text $"Counter = {n}")
        
        
        bulma.field.div [
            bulma.control.div [
                bulma.input.text [
                    Bind.attr("value", model .> (fun m -> m.Text), SetText >> dispatch)
                    Bind.attr("value", model .> (fun m -> m.Text), (fun x -> dispatch Increment ) )
                    Attr.required true]  ] ]
        
        Html.div [
            Html.button [
                class' "button"
                onClick (fun _ -> dispatch Decrement) []
                Html.text "-"
            ]

            Html.button [
                class' "button"
                onClick (fun _ -> dispatch Increment) []
                Html.text "+"
            ]
        ]
    ]

app() |> Program.mountElement "hegechat-app"