sqlcmd -S .\sqlexpress -U sa -P psWD!159! -i "c:\int\VOTGES\bat\reindexMin.sql"
sqlcmd -S .\sqlexpress -U sa -P psWD!159! -i "c:\int\VOTGES\bat\reindexSV.sql"
pause