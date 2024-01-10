import { sleep, check } from 'k6'
import http from 'k6/http'
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const options = {
  ext: {
    loadimpact: {
      distribution: { 'amazon:us:ashburn': { loadZone: 'amazon:us:ashburn', percent: 100 } },
      apm: [],
    },
  },
  thresholds: {
    http_req_duration: [{ threshold: 'avg<500', abortOnFail: true }],
    http_req_failed: [{ threshold: 'rate<=5', abortOnFail: true }],
  },
  scenarios: {
    Loadtest: {
      executor: 'ramping-vus',
      startTime: '1s',
      gracefulStop: '30s',
      stages: [
        { target: 5, duration: '1m' }
      ],
      gracefulRampDown: '30s',
      exec: 'loadtest',
    },
  },
}

export  function loadtest() {
  let response

  const vars = {}

 // group('BPCalculator - https://rakultudbpcalculator.azurewebsites.net/', function () {
    // 001_HomePage
    response = http.get('https://rakultudbpcalculator.azurewebsites.net/', {
      headers: {
        Accept:
          'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7',
        'Accept-Encoding': 'gzip, deflate, br',
        'Accept-Language': 'en-US,en;q=0.9',
        Connection: 'keep-alive',
        Host: 'rakultudbpcalculator.azurewebsites.net',
        'Sec-Fetch-Dest': 'document',
        'Sec-Fetch-Mode': 'navigate',
        'Sec-Fetch-Site': 'none',
        'Sec-Fetch-User': '?1',
        'Upgrade-Insecure-Requests': '1',
        'sec-ch-ua': '"Not_A Brand";v="8", "Chromium";v="120", "Google Chrome";v="120"',
        'sec-ch-ua-mobile': '?0',
        'sec-ch-ua-platform': '"Windows"',
      },
    })

    const element=response.html().find('input[name=__RequestVerificationToken]')
    const verificationcode = element.attr('value');

    sleep(8.5)

    
       response = http.post(
      'https://rakultudbpcalculator.azurewebsites.net/',
      {
        'BP.Systolic': '120',
        'BP.Diastolic': '80',
        __RequestVerificationToken: verificationcode,
      },
      {
        headers: {
          Accept:
            'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7',
          'Accept-Encoding': 'gzip, deflate, br',
          'Accept-Language': 'en-US,en;q=0.9',
          'Cache-Control': 'max-age=0',
          Connection: 'keep-alive',
          'Content-Type': 'application/x-www-form-urlencoded',
          Host: 'rakultudbpcalculator.azurewebsites.net',
          Origin: 'https://rakultudbpcalculator.azurewebsites.net',
          'Sec-Fetch-Dest': 'document',
          'Sec-Fetch-Mode': 'navigate',
          'Sec-Fetch-Site': 'same-origin',
          'Sec-Fetch-User': '?1',
          'Upgrade-Insecure-Requests': '1',
          'sec-ch-ua': '"Not_A Brand";v="8", "Chromium";v="120", "Google Chrome";v="120"',
          'sec-ch-ua-mobile': '?0',
          'sec-ch-ua-platform': '"Windows"',
        },
      }
    )

      //check(res, { res: 'status code is 200' }, { res: r => r.status === 200 })


  }
 


export function handleSummary(data) {
  return {
    "summary.html": htmlReport(data),
  };
}