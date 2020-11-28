using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PortalRadar
{
    public class PortalRadar : MonoBehaviour
    {
        private PortalRadar()
        {
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                // MKLogger.Log("Toggle Radar");
                this.ToggleRadar();
            }

            this.PrintTriggers();
        }

        private void Start()
        {
            this.subtitlePrefab = GlobalValueManager.main.defaultSubtitle;
        }

        private void RadarOn()
        {
            this.radar = Object.Instantiate<Subtitle>(this.subtitlePrefab);
            this.radar.Show("Radar On", false);
        }

        private void RadarOff()
        {
            Object.Destroy(this.radar.gameObject);
        }

        public void ToggleRadar()
        {
            //if (!ReferenceEquals(this.radar, null))
            if (this.radar != null)
            {
                this.RadarOff();
                return;
            }

            this.RadarOn();
        }

        private void Awake()
        {
            this.triggerList = new List<RadioTrigger>();
        }

        private void PrintTriggers()
        {
            //if (ReferenceEquals(this.radar, null))
            if(this.radar == null)
            {
                return;
            }

            string text = "";
            for (int i = this.triggerList.Count - 1; i >= 0; i--)
            {
                //if (ReferenceEquals(this.triggerList[i], null))
                if(this.triggerList[i] == null)
                {
                    this.triggerList.RemoveAt(i);
                }
                else
                {
                    text += this.triggerList[i].GetType().Name;
                    text += ": ";
                    text += (int)(this.triggerList[i].transform.position - PlayerCharacter.main.transform.position)
                        .sqrMagnitude;
                    text += "\n";
                }
            }

            text = text.Trim();
            if (text == "")
            {
                return;
            }

            this.radar.text.text = text;
        }

        public void RegisterPortal(RadioTrigger trigger)
        {
            if (!this.triggerList.Contains(trigger))
            {
                this.triggerList.Add(trigger);
            }
        }

        public Rect windowRect = new Rect(20f, 20f, 120f, 50f);

        private Subtitle subtitlePrefab;

        private Subtitle radar;

        private List<RadioTrigger> triggerList;
    }
}