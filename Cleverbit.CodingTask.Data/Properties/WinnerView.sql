select MatchResult.Id,Match.MatchName,MatchId, UserId , Winner.UserName
from( select MatchId, UserId,Id, ROW_NUMBER() OVER(PARTITION BY MatchId ORDER BY GeneratedNumber desc) as MatchResultPartition
from dbo.MatchResult ) as MatchResult
inner join dbo.Match as Match on Match.Id = MatchResult.MatchId
inner join dbo.[User] as Winner on Winner.Id = MatchResult.UserId
where MatchResultPartition = 1