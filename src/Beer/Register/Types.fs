module Beer.Register.Types

open Beer.Common.Types

type Model =
    {   Attendees : Attendee list
        Beers : Beer list
        AttendeeValue : string
        BeerValue : string }

type Msg =
    | AddAttendee
    | ChangeAttendeeValue of string
    | RemoveAttendee of attendeeId: int
    | AddBeer
    | ChangeBeerValue of string
    | RemoveBeer of beerId: int