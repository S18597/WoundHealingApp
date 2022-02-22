import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Pusher from 'pusher-js';
import { ChatDto } from 'src/app/DTOs/ChatDto';
import { MessageDto } from 'src/app/DTOs/MessageDto';
import { ApiService } from 'src/app/services/api.service';
import { Location } from '@angular/common';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  message!: MessageDto;
  messages!: MessageDto[];

  chats!: ChatDto[];
  selectedChat!: ChatDto;

  userId!: number | undefined;
  isPatient!: boolean | undefined;
  isDoctor!: boolean | undefined;

  selectedChatId!: number;
  msg = '';

  constructor(private api: ApiService, private router: Router, private _location: Location) {
    this.userId = api.loggedUserId;
    this.isPatient = api.loggedUserIsPatient;
    this.isDoctor = api.loggedUserIsDoctor;
  }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  async loadData() {
    console.log('load data');
    await this.loadChats();
    await this.loadChatMessages();
  }

  async loadChats() {
    console.log('load chats');
    this.chats = await this.api.getChats(this.userId, this.isPatient);
    console.log('loaded chats: ', this.chats);
    if(this.chats.length > 0){
      // choose first chat
      this.selectedChatId = this.chats[0].chatId;
      this.selectedChat = this.chats[0];
    }
  }

  async loadChatMessages() {
    if(this.selectedChatId > 0){
      console.log('load chat messages for selected chat id: ', this.selectedChatId);
      this.messages = await this.api.getChatMessages(this.selectedChatId, this.isPatient);
      console.log('loaded chat messages for selected chat: ', this.messages);
    }
  }

  async selectChat(chat: ChatDto) {
    console.log('select chat: ', chat);
    this.selectedChat = chat;
    this.selectedChatId = chat.chatId;
    await this.loadChatMessages();
  }

  async sendMessage() {
    console.log('send message: ', this.msg);
    let newMsg: MessageDto = {
      chatId: this.selectedChatId,
      doctorId: this.selectedChat.doctorId,
      doctorName: this.selectedChat.doctorName,
      patientId: this.selectedChat.patientId,
      patientName: this.selectedChat.patientName,
      message: this.msg,
      messageDate: new Date,
      isPatientMessage: this.isPatient
    }
    await this.api.sendMessage(newMsg);
    this.msg='';
  }

  ngOnInit(): void {
    Pusher.logToConsole = true;

    const pusher = new Pusher('ec988857c94670f54f9a', {
      cluster: 'eu'
    });

    const channel = pusher.subscribe('chat');
    channel.bind('message', (data: any )=> {
      this.messages.push(data);
    });

    (async () => {
      await this.loadData();
    })();
  }
}