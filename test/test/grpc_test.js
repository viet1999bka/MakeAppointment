import grpc from 'k6/net/grpc';
import { check, sleep } from 'k6';

const client = new grpc.Client();
client.load(['.'], 'example.proto');

export const options = {
    vus: 10, // số lượng người dùng ảo đồng thời
    duration: '30s', // thời gian chạy test
};

export default () => {
    client.connect('localhost:50051', {
        plaintext: true
    });

    const data = { exampleField: 'exampleValue' };

    const response = client.invoke('example.ExampleService/GetExample', data);

    check(response, {
        'status is OK': (r) => r && r.status === grpc.StatusOK,
        'response has message': (r) => r.message && r.message.message === 'expectedResponseMessage',
    });

    client.close();
    sleep(1);
};
