SET RESXSYNC="%~dp0Resxsync.exe"
SET RES=%~dp0..\Resources\
SET RESAG=%~dp0..\ResourcesAppGlobal\

REM Process Resources Folder
%RESXSYNC% "%RES%FBAPackChangePasswordWebPart.resx" *
%RESXSYNC% "%RES%FBAPackFeatures.resx" *
%RESXSYNC% "%RES%FBAPackMembershipRequestWebPart.resx" *
%RESXSYNC% "%RES%FBAPackMenus.resx" *
%RESXSYNC% "%RES%FBAPackPasswordRecoveryWebPart.resx" *

copy "%RES%FBAPackChangePasswordWebPart.resx" "%RES%FBAPackChangePasswordWebPart.en-US.resx"
copy "%RES%FBAPackFeatures.resx" "%RES%FBAPackFeatures.en-US.resx"
copy "%RES%FBAPackMembershipRequestWebPart.resx" "%RES%FBAPackMembershipRequestWebPart.en-US.resx"
copy "%RES%FBAPackMenus.resx" "%RES%FBAPackMenus.en-US.resx"
copy "%RES%FBAPackPasswordRecoveryWebPart.resx" "%RES%FBAPackPasswordRecoveryWebPart.en-US.resx"


REM Process ResourcesAppGlobal Folder

%RESXSYNC% "%RESAG%FBAPackWebPages.resx" *

copy "%RESAG%FBAPackWebPages.resx" "%RESAG%FBAPackWebPages.en-US.resx"