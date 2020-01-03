module Beer.Highscore.State

open Beer.Rating.Types
open Beer.Highscore.Types
open Elmish

let init() = { Highscore = [] }

let calculateHighscore (ratings: Beer.Rating.Types.Model) (registration: Beer.Register.Types.Model) =
  registration.Beers
  |> List.map (fun beer ->
    let tasteScore =
      registration.Attendees
      |> List.averageBy (fun rater ->
        float ratings.Ratings.[beer, rater, Taste])
    let lookScore =
      registration.Attendees
      |> List.averageBy (fun rater ->
        float ratings.Ratings.[beer, rater, Look])
    let containerScore =
      registration.Attendees
      |> List.averageBy (fun rater ->
        float ratings.Ratings.[beer, rater, Container])
    beer, (tasteScore*4. + lookScore*2. + containerScore)/7.)

let update (msg: Msg) (model: Model) (ratings: Beer.Rating.Types.Model) (registration: Beer.Register.Types.Model) =
    match msg with
    | Calculate ->
        { model with Highscore = calculateHighscore ratings registration }, Cmd.none