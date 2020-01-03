module Beer.Register.State

open Elmish
open Beer.Register.Types
open Beer.Common.Types

let private generateId =
    let mutable i = 0
    fun () ->
        i <- i+1
        i

let init() =
    {   Attendees = []
        Beers = []
        AttendeeValue = ""
        BeerValue = "" }

let update msg model =
    match msg with
    | AddAttendee ->
        let att = { AttendeeId = generateId(); Name = model.AttendeeValue }
        { model with Attendees = att::model.Attendees
                     AttendeeValue = "" }
        , Cmd.none
    | ChangeAttendeeValue(value) ->
        { model with AttendeeValue = value }, Cmd.none
    | RemoveAttendee(attendeeId) ->
        let beers =
            model.Beers
            |> List.filter (fun beer ->
                match beer.AttendeeId with
                | Some(attId) -> attId <> attendeeId
                | None -> true)

        { model with Attendees = model.Attendees
                                 |> List.filter (fun a -> a.AttendeeId <> attendeeId)
                     Beers = beers }
        , Cmd.none
    | AddBeer ->
        let beer = { BeerId = generateId(); Name = model.BeerValue; AttendeeId = None}
        { model with Beers = beer::model.Beers
                     BeerValue = "" }
        , Cmd.none
    | ChangeBeerValue(value) ->
        { model with BeerValue = value }, Cmd.none
    | RemoveBeer(beerId) ->
        { model with Beers = model.Beers
                             |> List.filter (fun b -> b.BeerId <> beerId) }
        , Cmd.none