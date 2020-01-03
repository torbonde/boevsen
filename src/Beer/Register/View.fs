module Beer.Register.View

open Beer.Register.Types
open Elmish
open Fulma
open Fable.React.Helpers
open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Beer.Common.Types

let hasABeer (model: Model) = model.Beers |> List.isEmpty |> not
let hasAnAttendee (model: Model) = model.Attendees |> List.isEmpty |> not

let root (model: Model) dispatch =
    Container.container [ ] [
        Section.section [ ] [
            Heading.p [ Heading.Is3 ] [ str "Tilføj smagere"]
            Field.div [ Field.HasAddons ] [
                Control.div [ ] [
                    Input.text [ Input.Placeholder "Navn"
                                 Input.Value model.AttendeeValue
                                 Input.Props [ OnChange (fun ev ->
                                                !!ev.target?value
                                                |> ChangeAttendeeValue
                                                |> dispatch)] ]
                ]
                Control.div [ ] [
                    Button.a [ Button.Color IsInfo
                               Button.OnClick (fun _ -> dispatch AddAttendee) ] [
                        str "Tilføj"
                    ]
                ]
            ]
            model.Attendees
            |> List.map (fun att ->
                Tag.tag [ Tag.Color IsInfo ] [
                    str att.Name
                    Delete.delete [ Delete.OnClick (fun _ ->
                        dispatch (RemoveAttendee(att.AttendeeId)))] []
                ])
            |> Tag.list [ ]
        ]

        Section.section [ ] [
            Heading.p [ Heading.Is3 ] [ str "Tilføj øl" ]
            Field.div [ Field.HasAddons ] [
                Control.div [ ] [
                    Input.text [ Input.Placeholder "Navn"
                                 Input.Value model.BeerValue
                                 Input.Props [ OnChange (fun ev ->
                                                !!ev.target?value
                                                |> ChangeBeerValue
                                                |> dispatch)] ]
                ]
                Control.div [ ] [
                    Button.a [ Button.Color IsInfo
                               Button.OnClick (fun _ -> dispatch AddBeer) ] [
                        str "Tilføj"
                    ]
                ]
            ]
            model.Beers
            |> List.map (fun beer ->
                Tag.tag [ Tag.Color IsInfo ] [
                    str (beer.ScreenName(model.Attendees))
                    Delete.delete [ Delete.OnClick (fun _ ->
                        dispatch (RemoveBeer(beer.BeerId)))] []
                ])
            |> Tag.list [ ]
        ]

        Section.section [ ] [
            Button.a [
                Button.IsFullWidth
                Button.Color IsInfo
                Button.Size IsLarge
                Button.Disabled (not (hasABeer model && hasAnAttendee model))
                Button.Props [ Router.href (Router.Beer(Router.BeerPage.Rating)) ]
            ] [
                str "Færdig"
            ]
        ]
    ]