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
            [ Label.label [] [ str ".tel"]
              Control.p [] [
                Input.tel [ Input.Placeholder "_/_/_" ]
            ] ]
          Field.div []
            [ Label.label [] [ str ".number"]
              Control.p [] [
                Input.number [ Input.Placeholder "_/_/_" ]
            ] ]
          Field.div []
            [ Label.label [] [ str ".month"]
              Control.p [] [
                Input.month [ Input.Placeholder "_/_/_" ]
            ] ]
          Field.div [ ]
            [ Label.label [ ]
                [ str "Subject" ]
              Control.div [ ]
                [ Select.select [ ]
                    [ select [  ]
                        [ option [ ] [ str "1" ]
                          option [ ] [ str "2" ]
                          option [ ] [ str "3" ] ] ] ] ] ]