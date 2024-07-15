import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
const client = new grpc.Client();
client.load(['.'], 'appointment.proto');

export function handleSummary(data) {
    return {
      "summary.html": htmlReport(data),
    };
  }

export const options = {
    stages: [
        { duration: '1m', target: 100 }, // Tăng dần số VUs lên 100 trong 1 phút
        { duration: '5m', target: 1000 }, // Tăng dần số VUs lên 100 trong 1 phút
        { duration: '10m', target: 5000 }, // Tăng dần số VUs lên 100 trong 1 phút
        // { duration: '2m', target: 5000 },  //Tăng dần số VUs lên 5000 trong 20 phút
    ],
    thresholds: {
        'grpc_req_duration': ['p(95)<10000'], // 95% của các yêu cầu phải dưới 500ms
        'grpc_req_duration': ['avg<15000'],   // Thời gian yêu cầu trung bình phải dưới 300ms
        'grpc_req_duration': ['max<20000'],  // Thời gian yêu cầu tối đa phải dưới 1000ms
    }
};

export default () => {
    client.connect('localhost:7238', {
        plaintext: false
    });

    const data = { 
        selectedId : 1,
        namePatient : "test từ k6",
        description : "k6 test tải",
        selectedDate : "7/12/2024",
        optionDate : "7/12/2024",
        nameDoctor : "test"
     };

    const response = client.invoke('appointment.AppointmentBookingApi/SetAppointmentBooking', data);
    // const response = client.invoke('appointment.AppointmentBookingApi/GetListAppointRegisted', {response : 1});
    // console.log(response.message);
    check(response.message, {
        // 'status is OK': (r) => r && r.reply === 1,
        'response has message': (r) => r.response === 1,
    });

    client.close();
    sleep(10);
};
