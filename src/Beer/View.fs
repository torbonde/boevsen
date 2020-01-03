module Beer.View

open Beer.Types

let root currentPage (model: Model) dispatch =
    match currentPage with
    | Router.BeerPage.Rating ->
        Rating.View.root model.RatingModel model.RegisterModel.Beers model.RegisterModel.Attendees (RatingDispatcherMsg >> dispatch)
    | Router.BeerPage.Registration ->
        Register.View.root model.RegisterModel (RegisterDispatcherMsg >> dispatch)
    | Router.BeerPage.Highscore ->
        Highscore.View.root model.HighscoreModel model.RegisterModel.Attendees (HighscoreDispatcherMsg >> dispatch)
