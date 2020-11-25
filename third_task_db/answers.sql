1. listázd azon userek nevét, akiknek nincs autója és a vezetékneve Kis :
SELECT user.name FROM `user`,user_car WHERE user_car.user = user.id and name LIKE "Kis%" GROUP BY user_car.user HAVING COUNT(user_car.user) = 0

2.listázd azon userek nevét, akiknek legalább 2 autója van	
SELECT user.name FROM `user`,user_car WHERE user_car.user = user.id GROUP BY user_car.user HAVING COUNT(user_car.user) > 1

2.1
backlog

3. szúrj be két oszlopot a user táblába: szemelyi_szam , nem
ALTER TABLE user
ADD szemelyi_szam varchar(11);
ALTER TABLE user
ADD nem BIT;
-- 1=férfi 0=nő

4.
INSERT INTO `car`(`brand`, `model`) VALUES ("Volkswagen","Arteon")
-- AI miatt nem kell id-t kitölteni