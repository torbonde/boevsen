module Beer.Rating.State

open Beer.Rating.Types
open Elmish
open Beer.Common.Types

let init() = { Ratings = Map.empty }

// let calculateHighscore model =
//   model.Beers
//   |> List.map (fun beerId ->
//     let tasteScore =
//       model.Attendees
//       |> List.averageBy (fun rater ->
//         float model.Ratings.[beerId, rater, Taste])
//     let lookScore =
//       model.Attendees
//       |> List.averageBy (fun rater ->
//         float model.Ratings.[beerId, rater, Look])
//     let containerScore =
//       model.Attendees
//       |> List.averageBy (fun rater ->
//         float model.Ratings.[beerId, rater, Container])
//     beerId, (tasteScore*4. + lookScore*2. + containerScore)/7.)

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
  match msg with
  | RateBeer(beerId, rater, aspect, rating) ->
    let key = (beerId, rater, aspect)
    let map =
      match Map.tryFind key model.Ratings with
      | Some r ->
        let map' = Map.remove key model.Ratings
        if r <> rating
        then Map.add key rating map'
        else map'
      | None -> Map.add key rating model.Ratings
    { model with Ratings = map }, Cmd.none