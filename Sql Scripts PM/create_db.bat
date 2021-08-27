ECHO off

sqlcmd -S localhost\sqlexpress  -E -i wheretowatch_db.sql
sqlcmd -S localhost\sqlexpress  -E -i movie_test_data.sql
sqlcmd -S localhost\sqlexpress  -E -i tv_test_data.sql
sqlcmd -S localhost\sqlexpress  -E -i trending_movie_test_data.sql
sqlcmd -S localhost\sqlexpress  -E -i trending_tv_test_data.sql
sqlcmd -S localhost\sqlexpress  -E -i movie_and_tv_favorites.sql

ECHO
ECHO if no error messages appear, DB was created.
pause