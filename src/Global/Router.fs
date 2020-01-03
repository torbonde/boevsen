module Router

open Browser
open Fable.React.Props
open Elmish.Navigation
open Elmish.UrlParser

type QuestionPage =
    | Index
    | Show of int
    | Create

type BeerPage =
    | Registration
    | Rating
    | Highscore

type Page =
    | Question of QuestionPage
    | Home
    | Beer of BeerPage

let private toHash page =
    match page with
    | Question questionPage ->
        match questionPage with
        | Index -> "#question/index"
        | Show id -> sprintf "#question/%i" id
        | Create -> "#question/create"
    | Home -> "#/"
    | Beer(Registration) -> "#/"
    | Beer(Rating) -> "#beer/rating"
    | Beer(Highscore) -> "#beer/highscore"

let pageParser: Parser<Page->Page,Page> =
    oneOf [
        map (QuestionPage.Index |> Question) (s "question" </> s "index")
        map (QuestionPage.Show >> Question) (s "question" </> i32)
        map (QuestionPage.Create |> Question) (s "question" </> s "create")
        map (Beer(Rating)) (s "beer" </> s "rating")
        map (Beer(Highscore)) (s "beer" </> s "highscore")
        // map (QuestionPage.Index |> Question) top
        map (Beer(Registration)) top ]

let href route =
    Href (toHash route)

let modifyUrl route =
    route |> toHash |> Navigation.modifyUrl

let newUrl route =
    route |> toHash |> Navigation.newUrl

let modifyLocation route =
    window.location.href <- toHash route
