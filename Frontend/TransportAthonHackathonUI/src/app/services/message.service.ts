import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Chat } from '../models/chat';
import { Message } from '../models/message';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable, Subject, from } from 'rxjs';

export type MessageRequest = {
  text: string
  receiverId: number
}

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private readonly httpClient = inject(HttpClient)

  private hubConnection: HubConnection
  private connectionUrl = `${environment.chatRoute}?Authorization=${localStorage.getItem('accessToken')}`;

  private readonly _receivedMessage: Subject<Message> = new Subject();

  connect() {
    this.startConnection();
    this.addListeners();
  }

  get receivedMessage$() {
    return this._receivedMessage.asObservable();
  }

  async disconnect() {
    await this.hubConnection.stop();
  }

  getChats() {
    return this.httpClient.get<Chat[]>(`${environment.apiUrl}Messages`)
  }

  getMessages(opponentUserId: number) {
    return this.httpClient.get<Message[]>(`${environment.apiUrl}Messages/${opponentUserId}`)
  }

  sendMessage(message: MessageRequest) {
    return this.httpClient.post<any>(`${environment.apiUrl}Messages`, message)
  }

  sendMessageToHub(message: MessageRequest) {
    var promise = this.hubConnection.invoke("SendMessageAsync", message)
      .then(() => { console.log('message sent successfully to hub'); })
      .catch((err) => console.log('error while sending a message to hub: ' + err));

    return from(promise);
  }

  private startConnection() {
    this.hubConnection = this.getConnection();

    this.hubConnection.start()
      .then(() => console.log('connection started'))
      .catch((err) => console.log('error while establishing signalr connection: ' + err))
  }

  private addListeners() {
    this.hubConnection.on("receiveMessage", (data: Message) => {
      console.log(data)
      this._receivedMessage.next(data)
    })
  }

  private getConnection(): HubConnection {
    return new HubConnectionBuilder()
      .withUrl(this.connectionUrl)
      .build();
  }
}
