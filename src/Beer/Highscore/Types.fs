module Beer.Highscore.Types

open Beer.Common.Types

type Model = { Highscore: (Beer * float) list }

type Msg = Calculate