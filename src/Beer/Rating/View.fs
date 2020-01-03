module Beer.Rating.View

open Beer.Rating.Types
open Elmish
open Fulma
open Fable.React.Helpers
open Fable.React
open Beer.Common.Types

[<AutoOpen>]
module Static =
    let aspects = [ Taste; Look; Container ]
    let ratings = [ Rating.One; Rating.Two; Rating.Three; Rating.Four; Rating.Five ]
    let colorMap =
      [ Rating.One  , IsDanger
        Rating.Two  , IsCustomColor "custom-orange"
        Rating.Three, IsWarning
        Rating.Four , IsCustomColor "custom-yellow-green"
        Rating.Five , IsSuccess ]
      |> Map.ofList


let allTriples list1 list2 list3 =
  list1
  |> List.collect (fun l1 ->
    list2
    |> List.collect (fun l2 ->
      list3
      |> List.map (fun l3 ->
        l1, l2, l3)))

let isAnyRatingMissing model beers attendees =
  (beers, attendees, aspects)
  |||> allTriples
  |> List.exists (not << model.Ratings.ContainsKey)

let isSelected beerId rater aspect rating model =
  Map.tryFind (beerId, rater, aspect) model.Ratings
  |> Option.map (fun r -> r = rating)
  |> Option.defaultValue false

let buttonList beerId rater aspect (model: Model) dispatch =
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

let aspectRating beerId rater aspect (model: Model) dispatch =
  Column.column [ Column.Width (Screen.Mobile, Column.IsOneThird) ] [
    str (aspect.ToString())
    buttonList beerId rater aspect model dispatch
  ]

let isAnyBeerRaterRatingMissing beerId rater model =
  aspects
  |> List.forall (fun a -> model.Ratings.ContainsKey(beerId, rater, a))
  |> not

let isAnyBeerRatingMissing beerId model attendees =
  aspects
  |> List.forall (fun a ->
    attendees
    |> List.forall (fun r ->
      model.Ratings.ContainsKey(beerId, r, a)))
  |> not


let rateBeerRaterView beerId rater (model: Model) dispatch =
  details [ Props.Open (isAnyBeerRaterRatingMissing beerId rater model) ] [
    summary [ ] [ Button.span [ Button.Color IsWhite] [ b [] [ str rater.Name ] ] ]
    Columns.columns [ Columns.IsMobile ] [
      aspectRating beerId rater Taste model dispatch
      aspectRating beerId rater Look model dispatch
      aspectRating beerId rater Container model dispatch
    ]
  ]

let rateBeerView (beer: Beer) (model: Model) attendees dispatch =
  details [ Props.Open (isAnyBeerRatingMissing beer model attendees) ] [
    yield summary [ ] [ Button.span [ Button.Color IsWhite ] [ Heading.p [ Heading.Is4 ] [ str (beer.ScreenName(attendees)) ]] ]
    yield! attendees |> List.map (fun rater -> rateBeerRaterView beer rater model dispatch)
  ]



let root (model: Model) beers attendees dispatch =
    Container.container [ ]
        [ Section.section [ ] [
            yield Heading.p [ Heading.Is3 ] [str "Giv karakter"]
            yield! beers |> List.map (fun beerId -> rateBeerView beerId model attendees dispatch)
            yield Section.section [ ] [ Button.a [
                    Button.IsFullWidth
                    Button.Color IsInfo
                    Button.Size IsLarge
                    Button.Disabled (isAnyRatingMissing model beers attendees)
                    Button.Props [ Router.href (Router.Beer(Router.BeerPage.Highscore)) ]
                  ] [
                    str "Beregn karakterer"
                  ] ]
        ] ]