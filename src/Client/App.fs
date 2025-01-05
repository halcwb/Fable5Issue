module App


open Fable.Core
open Browser
open Fable.React

let inline private toReact (el: JSX.Element) : ReactElement = unbox el


[<JSX.Component>]
let View () =


    let display showProgress (s: string) =

        if showProgress then
            JSX.jsx
                $"""
                import LinearProgress from '@mui/material/LinearProgress';
                <LinearProgress>{s}</LinearProgress>
                """
        else
            JSX.jsx
                $"""
                import Typography from '@mui/material/Typography';

                <React.Fragment>
                    <Typography variant="h6" gutterBottom >
                        {s}
                    </Typography>
                </React.Fragment>
                """

    let content = display true "Testing Fable 5"

    JSX.jsx
        $"""
        import React from 'react';

        <React.StrictMode>
            <React.Fragment>
                {content}
            </React.Fragment>
        </React.StrictMode>
    """


let app = ReactDomClient.createRoot (document.getElementById "app")
app.render (View() |> toReact)
