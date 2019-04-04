using System;

namespace Provider.UAApi
{
    public class FareLogixConstants
    {
    }

    public static class ConfigManagerUtil
    {
        


        /// <summary>
        /// the fms soap template
        /// </summary>
        private static string FmsSoapTemplate =
            @"<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'>
	            <SOAP-ENV:Header><t:Transaction xmlns:t='xxs'>
		            <tc source='CO'>
			            <iden u='COPOC' p='COPOC' pseudocity='AAL0' agt='copocuser01' agtpwd='3RzcGMry' agtrole='Ticketing Agent' agy='10770001'/>
			            <provider env='${Environments}'>United</provider>
			            <trace>UABags</trace>
		            </tc></t:Transaction>
	            </SOAP-ENV:Header>
	            <SOAP-ENV:Body>
		            <ns1:XXTransaction xmlns:ns1='xxs'>
			            <REQ>${ReqBody}</REQ>
		            </ns1:XXTransaction>
	            </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

       

        public static string FlxRequestTemplate = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tc='http://farelogix.com/flx/tc' xmlns:far='http://farelogix.com/flx/AirshoppingRQ'>
   <soapenv:Header>
      <tc:TransactionControl>
         <!--your TC credentials from Farelogix here.-->
           <tc>
            <iden u='Farelogix' p='TEMP02' pseudocity='AJJF' agt='xmlhack001' agtpwd='farelogix1' agtrole='Ticketing Agent' agy='00101640'/>
            <agent user='xmlhack001'/>
            <trace> AJJF_hack</trace>
            <script engine='FLXDM' name='uad-dispatch.flxdm'/>
         </tc>
      </tc:TransactionControl>
   </soapenv:Header>
   <soapenv:Body>
      <far:FlxTransaction>
         <!--please generate and use unique 32 digit TransactionIdentifier for each request-->
         ${ReqBody}
      </far:FlxTransaction>
   </soapenv:Body>
</soapenv:Envelope>";
    }
}
