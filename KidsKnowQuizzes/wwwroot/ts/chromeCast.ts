/// <reference types="chromecast-caf-receiver" />
/// <reference types="chrome" />

import '../scss/chromeCast.scss';

// @ts-ignore
import * as signalR from '@aspnet/signalr';

export class ChromeCast {
    static startApplication() {
        console.log("Starting application.");

        const signalRTest: HTMLDivElement = <HTMLDivElement>(document.querySelector("#signalRTest"));

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/hubs/quiz")
            .build();

        connection.start()
            .catch(err => console.error(err))
            .then(() => connection.send("registerTest", "ChromeCast"));

        connection.on("test", (message: string) => {
            let m = document.createElement("li");

            m.innerHTML =
                `<li class="list-group-item">${message}</li>`;

            signalRTest.appendChild(m);
            window.scrollTo(0, document.body.scrollHeight);
        });
    }
}