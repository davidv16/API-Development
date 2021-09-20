-- Population script of Technical Radiation
-- How to use: Copy all the following lines to SQLite Browser and execute them against your local database.

-- Populate news items
INSERT INTO NewsItems (Title, ImgSource, ShortDescription, LongDescription, PublishDate, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Taranto: Southern Italy''s hidden treasure', 'https://dynaimage.cdn.cnn.com/cnn/q_auto,w_900,c_fill,g_auto,h_506,ar_16:9/http%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F210827161008-01-taranto-italy-tourism.jpg', 'Located in the instep of Italy''s "boot," Tarantos heritage dates all the way back to the Spartans, who founded it in the 8th century BCE.', 'Taranto, Italy (CNN) — The Puglia region envelops the "heel" of the boot-shaped Italian peninsula. Laden with olive groves, surrounded by clear, sparkling water and speckled with charming, historic towns and villages, its rustic appeal has made it an increasingly popular destination for tourists.
The region''s list of must-see attractions includes the UNESCO World Heritage sites of Alberobello and Castel del Monte, the verdant national park of Gargano, the azure sea caves of Salento and quaint towns like Otranto, Ostuni and Gallipoli.', '2021-01-31', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO NewsItems (Title, ImgSource, ShortDescription, LongDescription, PublishDate, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Raelis Vasquez turns snapshots of Afro Dominican life into paintings of belonging', 'https://dynaimage.cdn.cnn.com/cnn/q_auto,w_1680,c_fill,g_auto,h_945,ar_16:9/http%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F210824183446-04-raelis-vasquez-paintings-hyphenated-restricted.jpg', 'This feature is part of CNN Style''s new series Hyphenated, which explores the complex issue of identity among minorities in the United States.', 'Raelis Vasquez, a New York and New Jersey-based artist, works from a bank of his memories and emotions to create tender and sincere paintings of Afro Dominican life. Usually featuring his kin and friends, the large-scale oil and acrylic works are cast in warm and welcoming colors. In one painting, a brown-skinned girl in a tubi, a tubular hairstyle crafted by Dominican women to preserve our salon blow-outs, eats her breakfast. In other works, a Black woman bottle-feeds her baby while sitting in a bright blue rocker, and a young Black couple await their destiny as they document their nuptials on their wedding day.', '2021-03-12', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO NewsItems (Title, ImgSource, ShortDescription, LongDescription, PublishDate, ModifiedBy, CreatedDate, ModifiedDate) VALUES('India posts record 20% GDP growth but the second Covid wave still hurt', 'https://cdn.cnn.com/cnnnext/dam/assets/210831045317-india-shopping-mall-0619-exlarge-169.jpg', 'Shoppers throng a mall in New Delhi in June after India eased coronavirus restrictions.', 'New Delhi (CNN Business)India''s economy grew at a record pace in the second quarter, even as the country grappled with a devastating wave of Covid-19. But Asia''s third largest economy isn''t out of the woods yet and remains smaller than at the start of the pandemic.

GDP rose 20.1% in the three months to June 30, compared with the same period a year earlier. The big jump reflects a rebound from the deep slump in activity in 2020, when Prime Minister Narendra Modi imposed one of the world''s toughest lockdowns on 1.3 billion people, restricting business and travel for months and plunging the country into its first recession in decades.', '2001-05-04', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO NewsItems (Title, ImgSource, ShortDescription, LongDescription, PublishDate, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Paris slams on the brakes and sets 30km/h speed limit to reduce pollution', 'https://cdn.cnn.com/cnnnext/dam/assets/210831105720-paris-speed-limit-0830-exlarge-169.jpg', 'A speed limit road sign in Paris on Monday as a rule change came into effect.', 'Paris (CNN)Authorities in the French capital of Paris are forcing drivers to slow down, setting a speed limit on almost all the city''s roads, in a bid to reduce pollution and improve road safety.

But will it work? There are questions over whether the rule -- which limits drivers to 30 kilometers per hour (19 miles per hour) -- will actually reduce pollution. Some critics say it will worsen the city''s rush-hour traffic jams and could even increase greenhouse gas emissions.
That will depend partly on whether drivers get so fed up with the speed limit that they choose to commute in other ways.', '2010-02-20', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO NewsItems (Title, ImgSource, ShortDescription, LongDescription, PublishDate, ModifiedBy, CreatedDate, ModifiedDate) VALUES('China bans kids from playing online video games during the week', 'https://cdn.cnn.com/cnnnext/dam/assets/210831003703-china-online-games-file-2019-exlarge-169.jpg', 'China has barred online gamers under the age of 18 from playing on weekdays and limited their play to just three hours most weekends, marking a significant escalation of restrictions on the country''s massive gaming industry.', 'Hong Kong/Beijing (CNN Business)China has barred online gamers under the age of 18 from playing on weekdays and limited their play to just three hours most weekends, marking a significant escalation of restrictions on the country''s massive gaming industry.

Starting this week, minors will be allowed only an hour of play time between 8 pm and 9 pm on Friday, weekends and public holidays, according to a statement from Chinese media watchdog — the National Press and Publication Administration (NPPA) — that was posted by state news agency Xinhua on Monday.
The move represents a huge tightening of earlier limits set by the agency in 2019, which had restricted play to 90 minutes on weekdays and three hours on weekends for children. Authorities said that the restrictions were put in place to help prevent young people becoming addicted to video games.
The NPPA noted this week that the rules were being issued "at the beginning of the new [school] semester, putting specific requirements for preventing the addiction to online games, and protecting the healthy growth of minors."
Investors were quick to react. NetEase (NTES) slumped 3.4% during regular trading hours in New York on Monday. Tencent (TCEHY) suffered roughly the same drop in Hong Kong on Tuesday before ticking back up 1.6%.', '2004-12-12', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

-- Populate authors
INSERT INTO Authors (Name, ProfileImgSource, Bio, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Jim Acosta', 'https://cdn.cnn.com/cnnnext/dam/assets/180501110147-jim-acosta-headshot-18-full-169.jpg', 'Abilio James Acosta is an American broadcast journalist, who is now an anchor and the chief domestic correspondent for CNN. Previously, Acosta served as the national political correspondent for CNN.', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Authors (Name, ProfileImgSource, Bio, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Jane Akre', 'https://pbs.twimg.com/profile_images/1510812542/IMG_8704_400x400.jpg', 'Jane Akre is an American journalist best known for the whistleblower lawsuit by herself and her former husband, Steve Wilson, against Fox station WTVT in Tampa, Florida. Akre and Wilson are featured in the 2003 documentary film The Corporation which includes a section about the same lawsuit.', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Authors (Name, ProfileImgSource, Bio, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Becky Anderson', 'https://pbs.twimg.com/profile_images/749963300037857280/agmbvprB_400x400.jpg', 'Rebecca Anderson known as Becky is a British journalist, and the anchor of CNN International''s flagship news and current affairs primetime news program Connect the World. She previously hosted Business International.', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

-- Populate categories
INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Business', 'business', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Health', 'health', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Tech', 'tech', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Travel', 'travel', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Traffic', 'traffic', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

INSERT INTO Categories (Name, Slug, ModifiedBy, CreatedDate, ModifiedDate) VALUES('Art', 'art', 'TechnicalRadiationAdmin', DATE('now'), DATE('now'));

-- Link authors to news items
INSERT INTO AuthorNewsItem (AuthorsId, NewsItemsId) VALUES(1, 1);
INSERT INTO AuthorNewsItem (AuthorsId, NewsItemsId) VALUES(2, 2);
INSERT INTO AuthorNewsItem (AuthorsId, NewsItemsId) VALUES(2, 3);
INSERT INTO AuthorNewsItem (AuthorsId, NewsItemsId) VALUES(3, 4);
INSERT INTO AuthorNewsItem (AuthorsId, NewsItemsId) VALUES(3, 5);

-- Link categories to news items
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(4, 1);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(6, 1);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(6, 2);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(2, 3);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(4, 3);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(5, 4);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(4, 4);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(1, 5);
INSERT INTO CategoryNewsItem (CategoriesId, NewsItemsId) VALUES(3, 5);
