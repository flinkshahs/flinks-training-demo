import { SecurityChallenges } from './SecurityChallenges';
export class Authorize_Response {
    Links: any;
    HttpStatusCode: number;
    SecurityChallenges: SecurityChallenges[];
    Institution: string;
    RequestId: string;
}