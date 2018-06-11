using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SASF.MVC.ViewModels
{
    public class CEPManager
    {
        public static CEPManagerViewModel ObterEndereco(string CEP)
        {
            //A PESQUISA DO GOOGLE PARA ENDEREÇO ATRAVÉS DO CEP NÃO TRAZ A RUA 
            // ENTÃO O PRIMEIRO PASSO É TRAZER A POSIÇÃO DE GPS PELO CEP E DEPOIS BUSCAR O ENDEREÇO DETALHANDO A RUA

            GPS gps = ObterGPS(CEP);
            if (gps != null)
                return ObterEndereco(gps);
            else
                return new CEPManagerViewModel();
        }

        public static GPS ObterGPS(string CEP)
        {
            string url = string.Format("https://maps.google.com/maps/api/geocode/xml?address={0}&key=AIzaSyBHhgNLk1ukliSjpzmp1ZRqfxfWnQ5Arss", CEP);
            XmlDocument doc = new XmlDocument();

            doc.Load(url);

            if (doc.SelectSingleNode("GeocodeResponse/status/text()").Value != "ZERO_RESULTS")
            {
                return new GPS
                {
                    Latitude = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lat/text()")),
                    Longitude = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/geometry/location/lng/text()")),
                    UF = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/address_component[type='administrative_area_level_1']/short_name/text()")),
                    Cidade = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/address_component[type='administrative_area_level_2']/short_name/text()")),
                    Bairro = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/address_component[type='sublocality_level_1']/short_name/text()"))
                };
            }
            else
                return new GPS();

        }

        public static CEPManagerViewModel obterAPIcorreios(string cep)
        {
            try
            {
                string url = string.Format("https://viacep.com.br/ws/{0}/xml/", cep);
                XmlDocument doc = new XmlDocument();

                doc.Load(url);

                return new CEPManagerViewModel
                {
                    Bairro = GetNdVal(doc.SelectSingleNode("xmlcep/bairro/text()")),
                    Logradouro = GetNdVal(doc.SelectSingleNode("xmlcep/logradouro/text()")),
                    CEP = GetNdVal(doc.SelectSingleNode("xmlcep/cep/text()")),
                    Cidade = GetNdVal(doc.SelectSingleNode("xmlcep/localidade/text()")),
                    UF = GetNdVal(doc.SelectSingleNode("xmlcep/uf/text()"))
                };
            }
            catch (Exception)
            {
                return new CEPManagerViewModel();
            }
        }


        public static CEPManagerViewModel ObterEndereco(GPS gps)
        {
            if (gps == null || (string.IsNullOrEmpty(gps.Latitude) && string.IsNullOrEmpty(gps.Longitude)))
                return new CEPManagerViewModel();

            string url = string.Format("https://maps.google.com/maps/api/geocode/xml?latlng={0},{1}&key=AIzaSyBHhgNLk1ukliSjpzmp1ZRqfxfWnQ5Arss", gps.Latitude, gps.Longitude);
            XmlDocument doc = new XmlDocument();

            doc.Load(url);

            if (doc.SelectSingleNode("GeocodeResponse/status/text()").Value == "OK")
            {
                return new CEPManagerViewModel
                {

                    Logradouro = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/address_component[type='route']/short_name/text()")),
                    CEP = GetNdVal(doc.SelectSingleNode("GeocodeResponse/result/address_component[type='postal_code']/short_name/text()")),
                    GPS = gps
                };
            }
            else
                return new CEPManagerViewModel();

        }

        private static string GetNdVal(XmlNode nd)
        {
            if (nd == null)
                return "";
            else
                return nd.Value.Trim();
        }
    }
}