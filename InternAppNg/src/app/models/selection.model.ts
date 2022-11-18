
import { Application } from "./application.model";
import { SelectionComment } from "./selection-comment.model";

export class Selection {
    public id: number;
    public name:string;
    public startDate: Date;
    public endDate: Date;
    public description:string;
    public selectionComments:SelectionComment[];
    public applications:Application[];
}
