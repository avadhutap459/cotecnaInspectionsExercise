import { Injectable } from '@angular/core';
import {
  DateAdapter, NativeDateAdapter
} from '@angular/material/core';

@Injectable()
export class CustomDateAdapter extends DateAdapter<Date | string> {
  adapter: NativeDateAdapter;
  private _today: Date = new Date();

  constructor(nativeDateAdapter: NativeDateAdapter) {
    super();
    this.adapter = nativeDateAdapter;
  }

  parse(value: any, parseFormat: any): string | Date | null {
    debugger
    if (value === 'permanent') {
      return 'permanent';
    }
    return this.adapter.parse(value, parseFormat);
  }
  isDateInstance(obj: any): boolean {
    debugger
    if (obj === 'permanent') {
      return true;
    }
    return this.adapter.isDateInstance(obj);
  }
  isValid(date: string | Date): boolean {
    debugger
    return date instanceof Date
      ? this.adapter.isValid(date)
      : date === 'permanent';
  }
  getYear(date: string | Date): number {
    debugger
    return date instanceof Date
      ? this.adapter.getYear(date)
      : this.adapter.getYear(this._today);
  }
  getMonth(date: string | Date): number {
    debugger
    return date instanceof Date
      ? this.adapter.getMonth(date)
      : this.adapter.getMonth(this._today);
  }
  getDate(date: string | Date): number {
    debugger
    return date instanceof Date
      ? this.adapter.getDate(date)
      : this.adapter.getDate(this._today);
  }
  getDayOfWeek(date: string | Date): number {
    return date instanceof Date
      ? this.adapter.getDayOfWeek(date)
      : this.adapter.getDayOfWeek(this._today);
  }
  getMonthNames(style: 'long' | 'short' | 'narrow'): string[] {
    return this.adapter.getMonthNames(style);
  }
  getDateNames(): string[] {
    return this.adapter.getDateNames();
  }
  getDayOfWeekNames(style: 'long' | 'short' | 'narrow'): string[] {
    return this.adapter.getDayOfWeekNames(style);
  }
  getYearName(date: string | Date): string {
    return date instanceof Date
      ? this.adapter.getYearName(date)
      : this.adapter.getYearName(this._today);
  }
  getFirstDayOfWeek(): number {
    return this.adapter.getFirstDayOfWeek();
  }
  getNumDaysInMonth(date: string | Date): number {
    return date instanceof Date
      ? this.adapter.getNumDaysInMonth(date)
      : this.adapter.getNumDaysInMonth(this._today);
  }
  clone(date: string | Date): string | Date {
    return date instanceof Date
      ? this.adapter.clone(date)
      : this.adapter.clone(this._today);
  }
  createDate(year: number, month: number, date: number): string | Date {
    return this.adapter.createDate(year, month, date);
  }
  today(): string | Date {
    return this.adapter.today();
  }
  format(date: string | Date, displayFormat: any): string {
    return date instanceof Date
      ? this.adapter.format(date, displayFormat)
      : (date as string);
  }
  addCalendarYears(date: string | Date, years: number): string | Date {
    return date instanceof Date
      ? this.adapter.addCalendarYears(date, years)
      : '';
  }
  addCalendarMonths(date: string | Date, months: number): string | Date {
    return date instanceof Date
      ? this.adapter.addCalendarMonths(date, months)
      : '';
  }
  addCalendarDays(date: string | Date, days: number): string | Date {
    return date instanceof Date ? this.adapter.addCalendarDays(date, days) : '';
  }
  toIso8601(date: string | Date): string {
    return date instanceof Date ? this.adapter.toIso8601(date) : '';
  }
  invalid(): string | Date {
    return this.adapter.invalid();
  }
}