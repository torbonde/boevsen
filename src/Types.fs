module App.Types

type Author =
    { Id : int
      Firstname: string
      Surname: string
      Avatar : string }

type Question =
    { Id : int
      Author : Author
      Title : string
      Description : string
      CreatedAt : string }

type Model =
    { CurrentPage : Router.Page
      Session : User
      QuestionDispatcher : Question.Dispatcher.Types.Model option
      BeerModel: Beer.Types.Model
      IsBurgerOpen : bool }

    static member Empty =
        { CurrentPage = Router.Beer(Router.Registration)
          Session =
            let userId = 3
            match Database.GetUserById userId with
            | Some user -> user
            | None -> failwithf "User#%i not found" userId
          QuestionDispatcher = None
          IsBurgerOpen = false
          BeerModel = Beer.State.init() }

type Msg =
    | QuestionDispatcherMsg of Question.Dispatcher.Types.Msg
    | BeerDispatcherMsg of Beer.Types.Msg
    | ResetDatabase
    | ToggleBurger
