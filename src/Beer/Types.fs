module Beer.Types

type Msg =
    | RegisterDispatcherMsg of Register.Types.Msg
    | RatingDispatcherMsg of Rating.Types.Msg
    | HighscoreDispatcherMsg of Highscore.Types.Msg

type Model =
    { RegisterModel: Register.Types.Model
      RatingModel: Rating.Types.Model
      HighscoreModel: Highscore.Types.Model }