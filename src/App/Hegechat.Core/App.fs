module Hegechat.Core.App

open Sutil

let app() =
    Html.div [
        Html.h1 "Hello Hegechat"
    ]

app() |> Program.mountElement "hegechat-app"