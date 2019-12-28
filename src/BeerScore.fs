module BeerScore.View

open Elmish
open Fulma
open Fable.React.Helpers
open Fable.React

type BeerId = int
type Rater = string
type Aspect = Taste | Look | Container with
  override this.ToString() =
    match this with
    | Taste -> "Smag/duft"
    | Look -> "Udseende"
    | Container -> "Flaske/dåse"

let aspects = [ Taste; Look; Container ]

type Rating =
  | One   = 1
  | Two   = 2
  | Three = 3
  | Four  = 4
  | Five  = 5

let ratings = [ Rating.One; Rating.Two; Rating.Three; Rating.Four; Rating.Five ]

let colorMap =
  [ Rating.One  , IsDanger
    Rating.Two  , IsCustomColor "custom-orange"
    Rating.Three, IsWarning
    Rating.Four , IsCustomColor "custom-yellow-green"
    Rating.Five , IsSuccess ]
  |> Map.ofList

type BeerScoreMsg =
  | RateBeer of BeerId * Rater * Aspect * Rating

type BeerScoreModel =
  { Ratings : Map<BeerId * Rater * Aspect, Rating>
    Raters: Rater list
    Beers: BeerId list }

  static member init() =
    { Ratings = Map.empty
      Raters = [ "Tor"; "Morten" ]
      Beers = [ 1; 2 ] }

let update (msg: BeerScoreMsg) (model: BeerScoreModel) : BeerScoreModel * Cmd<BeerScoreMsg> =
  let (RateBeer(beerId, rater, aspect, rating)) = msg
  { model with Ratings = Map.add (beerId, rater, aspect) rating model.Ratings }, Cmd.none

let isSelected beerId rater aspect rating model =
  Map.tryFind (beerId, rater, aspect) model.Ratings
  |> Option.map (fun r -> r = rating)
  |> Option.defaultValue false

let buttonList beerId rater aspect (model: BeerScoreModel) dispatch =
  ratings
  |> List.map (fun r ->
    Button.button [
      yield Button.IsFullWidth
      yield Button.Color colorMap.[r]
      if not(isSelected beerId rater aspect r model)
      then yield Button.IsOutlined
      yield Button.OnClick (fun _ -> RateBeer(beerId, rater, aspect, r) |> dispatch)
    ] [ r |> int |> string |> str ])
  |> Button.list [ ]

let aspectRating beerId rater aspect (model: BeerScoreModel) dispatch =
  Column.column [ Column.Width (Screen.Mobile, Column.IsOneThird) ] [
    str (aspect.ToString())
    buttonList beerId rater aspect model dispatch
  ]

let isAnyBeerRaterRatingMissing beerId rater model =
  aspects
  |> List.forall (fun a -> model.Ratings.ContainsKey(beerId, rater, a))
  |> not

let isAnyBeerRatingMissing beerId model =
  aspects
  |> List.forall (fun a ->
    model.Raters
    |> List.forall (fun r ->
      model.Ratings.ContainsKey(beerId, r, a)))
  |> not

let rateBeerRaterView beerId rater (model: BeerScoreModel) dispatch =
  details [ Props.Open (isAnyBeerRaterRatingMissing beerId rater model) ] [
    summary [ ] [ Button.span [ Button.Color IsWhite] [ b [] [ str rater ] ] ]
    Columns.columns [ Columns.IsMobile ] [
      aspectRating beerId rater Taste model dispatch
      aspectRating beerId rater Look model dispatch
      aspectRating beerId rater Container model dispatch
    ]
  ]

let rateBeerView (beerId: BeerId) (model: BeerScoreModel) dispatch =
  details [ Props.Open (isAnyBeerRatingMissing beerId model) ] [
    yield summary [ ] [ Button.span [ Button.Color IsWhite ] [ Heading.p [ Heading.Is4 ] [ str (sprintf "Øl %i" beerId) ]] ]
    yield! model.Raters |> List.map (fun rater -> rateBeerRaterView beerId rater model dispatch)
  ]

let root (model: BeerScoreModel) dispatch =
    Container.container [ ]
        [ Section.section [ ] [
            yield Heading.p [ Heading.Is3 ] [str "Giv karakter"]
            yield! model.Beers |> List.map (fun beerId -> rateBeerView beerId model dispatch)
        ] ]