
CLS

ECHO "=========="
if exist "~nubin" DEL /Q /F ~nubin
if exist "~nubin" RD ~nubin
MD ~nubin

COPY NKnife\bin\Release\NKnife.dll ~nubin\
COPY NKnife\bin\Release\NKnife.pdb ~nubin\

COPY NKnife.Channels\bin\Release\NKnife.Channels.dll ~nubin\
COPY NKnife.Channels\bin\Release\NKnife.Channels.pdb ~nubin\

COPY NKnife.DataLite\bin\Release\NKnife.DataLite.dll ~nubin\
COPY NKnife.DataLite\bin\Release\NKnife.DataLite.pdb ~nubin\

COPY NKnife.NLog4\bin\Release\NKnife.NLog4.dll ~nubin\
COPY NKnife.NLog4\bin\Release\NKnife.NLog4.pdb ~nubin\