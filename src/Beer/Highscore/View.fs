module Beer.Highscore.View

open Beer.Common.Types
open Beer.Highscore.Types
open Elmish
open Fulma
open Fable.React.Helpers
open Fable.React

let highscoreView (model: Model) attendees =
  match model.Highscore with
  | [] -> div [] []
  | hs ->
    hs
    |> List.sortByDescending snd
    |> List.map (fun (beer, score) ->
      li [] [
        str (sprintf  "%s; %.2f" (beer.ScreenName(attendees)) score)
      ])
    |> Content.Ol.ol []

let root model attendees dispatch =
    if List.isEmpty model.Highscore then dispatch(Calculate)
    Container.container [ ]
        [ Section.section [ ] [
            Heading.p [ Heading.Is3 ] [str "Giv karakter"]
            highscoreView model attendees
        ] ]