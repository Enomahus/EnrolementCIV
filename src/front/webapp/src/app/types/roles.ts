export const roles = [
  'SuperAdmin',
  'UsersAdministration',
  'VoterAdministration',
  'CandidateAdministration',
] as const;

export type Role = (typeof roles)[number];
