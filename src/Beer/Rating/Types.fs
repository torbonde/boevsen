module Beer.Rating.Types

open Beer.Common.Types

type Aspect = Taste | Look | Container with
  override this.ToString() =
    match this with
    | Taste -> "Smag/duft"
    | Look -> "Udseende"
    | Container -> "Flaske/dåse"

type Rating =
  | One   = 1
  | Two   = 2
  | Three = 3
  | Four  = 4
  | Five  = 5

type Model = { Ratings : Map<Beer * Attendee * Aspect, Rating> }

type Msg = RateBeer of Beer * Attendee * Aspect * Rating