import { Component, DestroyRef, ElementRef, OnDestroy, OnInit, ViewChild, effect, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageRequest, MessageService } from 'src/app/services/message.service';
import { Chat } from 'src/app/models/chat';
import { tap } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { Message } from 'src/app/models/message';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop'

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit, OnDestroy {
  private readonly messageService = inject(MessageService)
  private readonly toastrService = inject(ToastrService)
  private readonly destroyRef = inject(DestroyRef)

  @ViewChild('scrollMe') private myScrollContainer: ElementRef;

  chats$ = this.messageService.getChats();
  selectedChat = signal<Chat | null>(null);

  text: string | null = null;

  messages: Message[] = []

  constructor() {
    effect(() => {
      if (this.selectedChat() !== null) {
        this.messageService.getMessages(this.selectedChat()?.userId!)
          .pipe(
            tap(_ => setTimeout(() => {
              if (this.myScrollContainer) {
                this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
              }
            }, 0))
          ).subscribe({
            next: value => this.messages = value
          })
      }
    })
  }

  ngOnDestroy(): void {
    this.messageService.disconnect();
  }

  ngOnInit(): void {
    this.messageService.connect();

    this.messageService.receivedMessage$
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        tap(
          _ => setTimeout(() => {
            if (this.myScrollContainer) {
              this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
            }
          }, 0))
      )
      .subscribe({
        next: message => {
          this.text = null;
          if (this.selectedChat() && this.selectedChat()?.userId === message.senderId || this.selectedChat()?.userId === message.receiverId) {
            this.messages.push(message);
          }
        }
      })
  }

  selectChat(chat: Chat) {
    this.selectedChat.set(chat)
  }

  sendMessage() {
    if (this.selectedChat() === null) {
      this.toastrService.warning("İlk önce bir sohbet seçmelisiniz.", "Uyarı")
      return;
    }

    if (this.text === null) {
      return
    }

    const body: MessageRequest = {
      text: this.text,
      receiverId: this.selectedChat()!.userId
    }

    this.messageService.sendMessageToHub(body).subscribe();

    /* this.messageService.sendMessage(body).subscribe({
      next: _ => {
        this.text = null;
        this.messageService.getMessages(this.selectedChat()!.userId).pipe(tap(
          _ => setTimeout(() => {
            if (this.myScrollContainer) {
              this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
            }
          }, 0)
        )).subscribe({
          next: value => this.messages = value
        })
      }
    }) */
  }
}