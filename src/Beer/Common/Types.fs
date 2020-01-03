module Beer.Common.Types

type Attendee =
    {   AttendeeId: int
        Name: string }

type Beer =
    {   BeerId: int
        Name: string
        AttendeeId: int option } with

    member this.ScreenName(attendees : Attendee list) =
        this.AttendeeId
        |> Option.bind (fun attId ->
            attendees
            |> List.tryFind (fun att -> att.AttendeeId = attId))
        |> Option.map (fun att ->
            sprintf "%s (%s)" this.Name att.Name)
        |> Option.defaultValue this.Name