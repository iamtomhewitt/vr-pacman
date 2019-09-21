import requests
import json

get = requests.get('http://dreamlo.com/lb/5ad0d623d6024519e0be327e/json')
j = json.loads(get.text)
entries = j['dreamlo']['leaderboard']['entry']

for entry in entries:
    name = entry['name'][14:]
    name = name.replace('+', ' ')
    name = name.replace("'", ' ')
    score = entry['score']
    post_url = 'http://dreamlo.com/lb/YHc0fAncbE-2ieJokE0NIAB9ZEzv8l10WovytuLwzMAw/clear'
    # requests.get(post_url);
    post_url = 'http://dreamlo.com/lb/YHc0fAncbE-2ieJokE0NIAB9ZEzv8l10WovytuLwzMAw/add/'+name+'/'+score
    # requests.get(post_url);

# {
#     "dreamlo": {
#         "leaderboard": {
#             "entry": [
#                 {
#                     "name": "09012019074109Julius",
#                     "score": "25800",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/1/2019 4:41:10 PM"
#                 },
#                 {
#                     "name": "04202019095906Julius",
#                     "score": "21990",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/20/2019 6:59:07 PM"
#                 },
#                 {
#                     "name": "08052018085813GHOST",
#                     "score": "17120",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/5/2018 10:58:14 AM"
#                 },
#                 {
#                     "name": "05122019071050Aiden+JJH",
#                     "score": "15350",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/12/2019 10:10:50 AM"
#                 },
#                 {
#                     "name": "05272018105014B-C",
#                     "score": "14200",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/27/2018 3:50:15 PM"
#                 },
#                 {
#                     "name": "08202018054717nick+the+g",
#                     "score": "13880",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/20/2018 8:47:00 PM"
#                 },
#                 {
#                     "name": "08092018081629joaogamer3",
#                     "score": "13460",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/10/2018 12:16:31 AM"
#                 },
#                 {
#                     "name": "11052018123622artem",
#                     "score": "13420",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "11/5/2018 9:36:23 AM"
#                 },
#                 {
#                     "name": "08242019101242XNXX",
#                     "score": "13400",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/25/2019 3:12:42 AM"
#                 },
#                 {
#                     "name": "05252018114255deven",
#                     "score": "13040",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/26/2018 3:42:54 AM"
#                 },
#                 {
#                     "name": "06202018010914Yommy",
#                     "score": "11590",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/20/2018 6:09:15 AM"
#                 },
#                 {
#                     "name": "11042018091606oceane",
#                     "score": "11460",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "11/4/2018 8:16:04 PM"
#                 },
#                 {
#                     "name": "07042018041129clint",
#                     "score": "11430",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/4/2018 9:11:32 PM"
#                 },
#                 {
#                     "name": "08062018124243vann",
#                     "score": "11030",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/6/2018 5:42:44 PM"
#                 },
#                 {
#                     "name": "06182018101238fatfat",
#                     "score": "10970",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/19/2018 3:12:40 AM"
#                 },
#                 {
#                     "name": "04282018053935jayy",
#                     "score": "10810",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/28/2018 9:39:36 PM"
#                 },
#                 {
#                     "name": "08232019012337Natalia15",
#                     "score": "10570",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/23/2019 11:23:37 AM"
#                 },
#                 {
#                     "name": "06232018095851Dadiel",
#                     "score": "10560",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/23/2018 8:58:52 PM"
#                 },
#                 {
#                     "name": "03052019075045domi",
#                     "score": "10550",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "3/5/2019 6:47:59 PM"
#                 },
#                 {
#                     "name": "05202018021054Dj+fox",
#                     "score": "10480",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/20/2018 7:12:45 PM"
#                 },
#                 {
#                     "name": "04272018103233anthony",
#                     "score": "10460",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/28/2018 5:32:34 AM"
#                 },
#                 {
#                     "name": "01042019042553Nicolas",
#                     "score": "10160",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "1/4/2019 9:25:55 PM"
#                 },
#                 {
#                     "name": "07272018075713APPLEG8_06",
#                     "score": "10150",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/28/2018 2:57:12 AM"
#                 },
#                 {
#                     "name": "07142018042148Mc.Donalds",
#                     "score": "10130",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/14/2018 10:21:46 PM"
#                 },
#                 {
#                     "name": "06032018100807artem",
#                     "score": "9860",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/3/2018 7:08:07 AM"
#                 },
#                 {
#                     "name": "05232018082646Natalie",
#                     "score": "9640",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/24/2018 3:26:47 AM"
#                 },
#                 {
#                     "name": "09202018040420thejocjker",
#                     "score": "9500",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/20/2018 9:04:20 PM"
#                 },
#                 {
#                     "name": "08092018085839Gabriel",
#                     "score": "9450",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/10/2018 12:58:40 AM"
#                 },
#                 {
#                     "name": "05182018115811Natalie",
#                     "score": "9450",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/18/2018 6:58:11 PM"
#                 },
#                 {
#                     "name": "04292018011600emily",
#                     "score": "9050",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/29/2018 5:16:01 PM"
#                 },
#                 {
#                     "name": "04192019044202ARRIBA+ESP",
#                     "score": "9030",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/19/2019 2:42:02 PM"
#                 },
#                 {
#                     "name": "08312019114544Cristidesk",
#                     "score": "8960",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/1/2019 4:45:26 AM"
#                 },
#                 {
#                     "name": "08032019022609dan",
#                     "score": "8780",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/3/2019 10:28:18 AM"
#                 },
#                 {
#                     "name": "12272018093100rafa",
#                     "score": "8740",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "12/28/2018 3:31:01 AM"
#                 },
#                 {
#                     "name": "06132018092658no+life",
#                     "score": "8680",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/13/2018 4:26:58 PM"
#                 },
#                 {
#                     "name": "08212018010002ACE_21",
#                     "score": "8560",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/21/2018 6:00:09 PM"
#                 },
#                 {
#                     "name": "07032018095454Josh",
#                     "score": "8550",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/4/2018 1:54:54 AM"
#                 },
#                 {
#                     "name": "09082018122031rnone",
#                     "score": "8330",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/8/2018 5:20:33 PM"
#                 },
#                 {
#                     "name": "02072019064642Qhaleef",
#                     "score": "8280",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "2/7/2019 10:46:42 AM"
#                 },
#                 {
#                     "name": "12082018061816Loïc",
#                     "score": "8240",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "12/8/2018 5:18:16 PM"
#                 },
#                 {
#                     "name": "04142018085543Ben",
#                     "score": "8220",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/15/2018 1:55:48 AM"
#                 },
#                 {
#                     "name": "05012018025939JoseRojas",
#                     "score": "8150",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/1/2018 8:00:50 PM"
#                 },
#                 {
#                     "name": "07252018114406David",
#                     "score": "8080",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/26/2018 4:44:08 AM"
#                 },
#                 {
#                     "name": "12082018105242Bob",
#                     "score": "8080",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "12/8/2018 12:52:42 AM"
#                 },
#                 {
#                     "name": "05242018085039DanDoes",
#                     "score": "8030",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/24/2018 7:50:37 PM"
#                 },
#                 {
#                     "name": "07292018042615honeybee",
#                     "score": "7870",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/29/2018 4:26:16 AM"
#                 },
#                 {
#                     "name": "09222018111206emi's",
#                     "score": "7760",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/23/2018 5:12:04 AM"
#                 },
#                 {
#                     "name": "05092018061119joey",
#                     "score": "7610",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/9/2018 10:11:20 PM"
#                 },
#                 {
#                     "name": "05082018104937jayy",
#                     "score": "7600",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/8/2018 2:49:39 PM"
#                 },
#                 {
#                     "name": "06022018081306liam",
#                     "score": "7470",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/2/2018 12:13:06 PM"
#                 },
#                 {
#                     "name": "08042018100731hunter",
#                     "score": "7340",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/4/2018 3:07:31 PM"
#                 },
#                 {
#                     "name": "11112018055207elo",
#                     "score": "7340",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "11/11/2018 4:52:06 PM"
#                 },
#                 {
#                     "name": "07292019035055Eike",
#                     "score": "7340",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/29/2019 6:50:56 PM"
#                 },
#                 {
#                     "name": "05172018103417BlueZ",
#                     "score": "7330",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/17/2018 5:34:16 PM"
#                 },
#                 {
#                     "name": "06212018073405I'm+a+king",
#                     "score": "7320",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/22/2018 12:34:06 AM"
#                 },
#                 {
#                     "name": "09022018105025sacpuma",
#                     "score": "7320",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/2/2018 8:50:25 PM"
#                 },
#                 {
#                     "name": "08122019061150Loe",
#                     "score": "7240",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/12/2019 11:11:51 PM"
#                 },
#                 {
#                     "name": "05202018034739shay",
#                     "score": "7210",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/20/2018 7:47:40 AM"
#                 },
#                 {
#                     "name": "04262018053836jon",
#                     "score": "7120",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/26/2018 10:38:37 PM"
#                 },
#                 {
#                     "name": "12212018081627DCViggo69",
#                     "score": "7090",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "12/21/2018 7:16:28 PM"
#                 },
#                 {
#                     "name": "07182018072645toni",
#                     "score": "7050",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/18/2018 9:56:46 PM"
#                 },
#                 {
#                     "name": "07252018032649Sans",
#                     "score": "7040",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/25/2018 8:26:48 PM"
#                 },
#                 {
#                     "name": "05122018055915dad",
#                     "score": "6950",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/13/2018 12:57:04 AM"
#                 },
#                 {
#                     "name": "11032018012624DEPREMU",
#                     "score": "6830",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "11/3/2018 8:26:24 PM"
#                 },
#                 {
#                     "name": "04022019023517Heinstein",
#                     "score": "6820",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/2/2019 1:34:58 PM"
#                 },
#                 {
#                     "name": "04052019104623aniel",
#                     "score": "6710",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/5/2019 7:46:23 PM"
#                 },
#                 {
#                     "name": "08092019093357oof",
#                     "score": "6660",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/10/2019 12:33:58 AM"
#                 },
#                 {
#                     "name": "05132018023209NIGHTMARE",
#                     "score": "6650",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/13/2018 5:32:09 AM"
#                 },
#                 {
#                     "name": "07252019121139max",
#                     "score": "6640",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/24/2019 5:11:40 PM"
#                 },
#                 {
#                     "name": "06222018105952GamerSlime",
#                     "score": "6520",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/23/2018 2:59:52 AM"
#                 },
#                 {
#                     "name": "02242019124239jm22",
#                     "score": "6520",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "2/24/2019 3:41:15 AM"
#                 },
#                 {
#                     "name": "08182018071713daddy",
#                     "score": "6490",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/19/2018 2:17:14 AM"
#                 },
#                 {
#                     "name": "04282018104210Goku",
#                     "score": "6390",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/28/2018 2:42:11 PM"
#                 },
#                 {
#                     "name": "06012019113705Pikachu",
#                     "score": "6350",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/1/2019 4:37:05 PM"
#                 },
#                 {
#                     "name": "01092019122642Patrice+S",
#                     "score": "6330",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "1/9/2019 5:26:43 AM"
#                 },
#                 {
#                     "name": "04172018053438jessie",
#                     "score": "6310",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/17/2018 9:34:21 PM"
#                 },
#                 {
#                     "name": "07272018040609T+OF+OMG",
#                     "score": "6290",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/27/2018 10:06:10 PM"
#                 },
#                 {
#                     "name": "04212019062620vetle",
#                     "score": "6270",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/21/2019 4:26:21 PM"
#                 },
#                 {
#                     "name": "08032018083635mario+yea$",
#                     "score": "6230",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/4/2018 1:36:36 AM"
#                 },
#                 {
#                     "name": "06302018115930Dyaminnell",
#                     "score": "6220",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/30/2018 3:59:32 AM"
#                 },
#                 {
#                     "name": "05072018084221leon7705",
#                     "score": "6210",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/7/2018 11:41:58 PM"
#                 },
#                 {
#                     "name": "08082019032428random",
#                     "score": "6170",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/8/2019 12:24:29 PM"
#                 },
#                 {
#                     "name": "12242018011910Nicklash",
#                     "score": "6140",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "12/24/2018 3:21:04 PM"
#                 },
#                 {
#                     "name": "02052019083811Genaro",
#                     "score": "6080",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "2/6/2019 1:37:39 AM"
#                 },
#                 {
#                     "name": "06142018072051Pacistan",
#                     "score": "6020",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/14/2018 5:19:29 PM"
#                 },
#                 {
#                     "name": "07062019060539gabriel",
#                     "score": "6000",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/6/2019 9:05:41 PM"
#                 },
#                 {
#                     "name": "06162018090229MJJKOP",
#                     "score": "5990",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/16/2018 1:02:30 PM"
#                 },
#                 {
#                     "name": "04272018094636Butt",
#                     "score": "5960",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "4/28/2018 1:46:37 AM"
#                 },
#                 {
#                     "name": "09082018035946gavin",
#                     "score": "5950",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "9/8/2018 8:59:46 PM"
#                 },
#                 {
#                     "name": "06172018044400iesha",
#                     "score": "5930",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/17/2018 8:44:01 PM"
#                 },
#                 {
#                     "name": "08102019102723димон",
#                     "score": "5890",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/10/2019 7:27:27 PM"
#                 },
#                 {
#                     "name": "08222018085844Link",
#                     "score": "5880",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/22/2018 7:58:45 AM"
#                 },
#                 {
#                     "name": "07132018085706tk",
#                     "score": "5840",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/14/2018 12:57:08 AM"
#                 },
#                 {
#                     "name": "08192019100410Lexxo07",
#                     "score": "5830",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/19/2019 8:04:42 PM"
#                 },
#                 {
#                     "name": "07172018111923jayyy",
#                     "score": "5830",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "7/18/2018 6:19:23 AM"
#                 },
#                 {
#                     "name": "08062018081643vann",
#                     "score": "5790",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "8/6/2018 1:16:44 PM"
#                 },
#                 {
#                     "name": "10082018103756fredy006",
#                     "score": "5740",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "10/8/2018 8:37:57 PM"
#                 },
#                 {
#                     "name": "06112018111107The+BMV",
#                     "score": "5730",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "6/12/2018 3:11:06 AM"
#                 },
#                 {
#                     "name": "01262019044752Abby",
#                     "score": "5720",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "1/26/2019 9:47:54 PM"
#                 },
#                 {
#                     "name": "05192018090648The3rdTrap",
#                     "score": "5690",
#                     "seconds": "0",
#                     "text": "",
#                     "date": "5/19/2018 1:06:49 PM"
#                 }
#             ]
#         }
#     }
# }