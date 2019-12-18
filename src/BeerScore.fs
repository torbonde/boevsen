module BeerScore.View

open Fulma
open Fable.React.Helpers
open Fable.React

let root user model dispatch =
    Container.container []
        [ Heading.h3 [] [str "Hello"]
          Field.div []
            [ Label.label [ ]
                [ str "Name" ]
              Control.div [ ]
                [ Input.text [ Input.Placeholder "Ex: Maxime" ] ] ]
          Field.div []
            [ Control.p [] [
                Input.number [ ]
            ] ] ]