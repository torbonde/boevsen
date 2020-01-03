module Beer.State

open Beer.Types
open Elmish

let init () =
    { RatingModel = Rating.State.init()
      RegisterModel = Register.State.init()
      HighscoreModel = Highscore.State.init() }

let update msg model =
    match msg with
    | RegisterDispatcherMsg msg' ->
        let subModel, cmd = Register.State.update msg' model.RegisterModel
        { model with RegisterModel = subModel }, Cmd.map RegisterDispatcherMsg cmd
    | RatingDispatcherMsg msg' ->
        let subModel, cmd = Rating.State.update msg' model.RatingModel
        { model with RatingModel = subModel }, Cmd.map RatingDispatcherMsg cmd
    | HighscoreDispatcherMsg msg' ->
        let subModel, cmd = Highscore.State.update msg' model.HighscoreModel model.RatingModel model.RegisterModel
        { model with HighscoreModel = subModel }, Cmd.map HighscoreDispatcherMsg cmd